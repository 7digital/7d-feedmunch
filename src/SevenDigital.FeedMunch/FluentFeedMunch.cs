using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DeCsv;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds;
using SevenDigital.Api.FeedReader.Feeds.Track;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace SevenDigital.FeedMunch
{
	public class FluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly FeedReader _feedReader;
		private readonly IFileHelper _fileHelper;
		private readonly IEventAdapter _logEvent;

		public FeedMunchConfig Config { get; private set; }

		public FluentFeedMunch(IFeedDownload feedDownload, FeedReader feedReader, IFileHelper fileHelper, IEventAdapter logEvent)
		{
			_feedDownload = feedDownload;
			_feedReader = feedReader;
			_fileHelper = fileHelper;
			_logEvent = logEvent;
		}

		public FluentFeedMunch WithConfig(FeedMunchConfig config)
		{
			Config = config;
			return this;
		}

		public void Invoke()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var feed = new Feed(Config.Feed, Config.Catalog) { Country = Config.Country };

			_logEvent.Info(feed.ToString());

			var saveLocally = _feedDownload.SaveLocally(feed);

			_logEvent.Info(string.Format("Downloading to {0}", _feedDownload.CurrentFileName));

			var timer = ConsoleFilePolling.GenerateFileSizePollingTimer(_feedDownload.CurrentFileName, 300);
			
			saveLocally.ContinueWith(task =>
			{
				_logEvent.Info("Finished downloading");
				stopwatch.Stop();
				_logEvent.Info(String.Format("Took {0} milliseconds to download", stopwatch.ElapsedMilliseconds));
				
				FilterFeedAndWrite(feed);
				
				timer.Dispose();
			}, TaskContinuationOptions.LongRunning);
		}

		private void FilterFeedAndWrite(Feed feed)
		{
			var rows = ReadAllRows(feed);
			var filteredFeed = FilterRows(rows);
			var outputFeedPath = GenerateOutputFeedLocation(Config.Output);

			_logEvent.Info(string.Format("Writing filtered feed out to {0}", outputFeedPath));

			var timeFilteredFeedWrite = TimerHelper.TimeMe(() => TryOutputFilteredFeed(outputFeedPath, filteredFeed));

			_logEvent.Info(string.Format("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds));
		}

		private static void TryOutputFilteredFeed(string outputFeedPath, IEnumerable<object> filteredFeed)
		{
			try
			{
				using (var compressedFileStream = File.Create(outputFeedPath))
				{
					using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
					{
						CsvSerializer.SerializeToStream(filteredFeed, compressionStream);
					}
				}
				TryChangeExtension(outputFeedPath, ".tmp", ".gz");
			}
			catch (CsvDeserializationException ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private string GenerateOutputFeedLocation(string output)
		{
			_fileHelper.GetOrCreateFeedsFolder();
			var filename = Path.GetFileNameWithoutExtension(output);
			var directoryPath = Path.GetDirectoryName(output);
			var outputDirectory = _fileHelper.GetOrCreateOutputFolder(directoryPath);
			return Path.Combine(outputDirectory, filename + ".tmp");
		}

		private IEnumerable<object> ReadAllRows(Feed feed)
		{
			// TODO - trial hard loading this into ram before processing as this may be bottlenecked by disk IO

			_logEvent.Info("Reading data into list");
			var readIntoList = _feedReader.ReadIntoList(feed);
			return Config.Limit > 0 
				? readIntoList.Take(Config.Limit) 
				: readIntoList;
		}

		private IEnumerable<object> FilterRows(IEnumerable<object> rows)
		{
			var filter = new Filter(Config.Filter);
			return rows.Where(filter.ApplyToRow);
		}

		private static void TryChangeExtension(string path, string from, string to)
		{
			var completedFilePath = path.Replace(from, to);
			if (File.Exists(completedFilePath))
			{
				File.Delete(completedFilePath);
			}
			File.Move(path, completedFilePath);
		}
	}
}