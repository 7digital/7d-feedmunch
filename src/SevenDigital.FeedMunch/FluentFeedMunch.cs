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
using SevenDigital.Api.FeedReader.Feeds.Schema;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace SevenDigital.FeedMunch
{
	public class FluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly GenericFeedReader _genericFeedReader;
		private readonly IFileHelper _fileHelper;
		private readonly IEventAdapter _logEvent;

		public FeedMunchConfig Config { get; private set; }

		public FluentFeedMunch(IFeedDownload feedDownload, GenericFeedReader genericFeedReader, IFileHelper fileHelper, IEventAdapter logEvent)
		{
			_feedDownload = feedDownload;
			_genericFeedReader = genericFeedReader;
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

			//var timer = ConsoleFilePolling.GenerateFileSizePollingTimer(_feedDownload.CurrentFileName, 300);
			
			saveLocally.ContinueWith(task =>
			{
				if (task.Exception != null)
				{
					_logEvent.Info("An error occured downloading the feed");
					_logEvent.Info(task.Exception.InnerExceptions.First().Message);
					return;
				}

				_logEvent.Info("Finished downloading");
				stopwatch.Stop();
				_logEvent.Info(String.Format("Took {0} milliseconds to download", stopwatch.ElapsedMilliseconds));
				
				FilterFeedAndWrite(feed);
				
				//timer.Dispose();
			}, TaskContinuationOptions.LongRunning);
		}

		private void FilterFeedAndWrite(Feed feed)
		{
			var filteredFeed = ReadAndFilter(feed);
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

		private dynamic ReadAndFilterAllRows<T>(Feed feed)
		{
			_logEvent.Info("Reading data into list");
			var readIntoList = _genericFeedReader.ReadIntoList<T>(feed);
			var rows = Config.Limit > 0 ? readIntoList.Take(Config.Limit) : readIntoList;
			var filter = new Filter(Config.Filter);
			return rows.Where(row => filter.ApplyToRow(row));
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

		private dynamic ReadAndFilter(Feed feed)
		{
			if (feed.CatalogueType == FeedCatalogueType.Artist && feed.FeedType == FeedType.Full)
			{
				return ReadAndFilterAllRows<Artist>(feed);
			}
			if (feed.CatalogueType == FeedCatalogueType.Artist && feed.FeedType == FeedType.Incremental)
			{
				return ReadAndFilterAllRows<ArtistIncremental>(feed);
			}
			if (feed.CatalogueType == FeedCatalogueType.Release && feed.FeedType == FeedType.Full)
			{
				return ReadAndFilterAllRows<Release>(feed);
			}
			if (feed.CatalogueType == FeedCatalogueType.Release && feed.FeedType == FeedType.Incremental)
			{
				return ReadAndFilterAllRows<ReleaseIncremental>(feed);
			}
			if (feed.CatalogueType == FeedCatalogueType.Track && feed.FeedType == FeedType.Full)
			{
				return ReadAndFilterAllRows<Track>(feed);
			}
			return ReadAndFilterAllRows<TrackIncremental>(feed);

		}
	}
}