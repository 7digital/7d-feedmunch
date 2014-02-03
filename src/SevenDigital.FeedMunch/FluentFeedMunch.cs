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
using SevenDigital.Api.FeedReader.Feeds.Track;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace SevenDigital.FeedMunch
{
	/// <summary>
	/// WIP - The idea for this class is that will eventually be the Fluent "Facade" for downloading and filtering feeds
	/// Began as a spike so need to pull apart this logic to test
	/// </summary>
	public class FluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly TrackFeedReader _trackFeedReader;
		private readonly IFileHelper _fileHelper;
		private readonly IEventAdapter _logEvent;

		public FeedMunchConfig Config { get; private set; }

		public FluentFeedMunch(IFeedDownload feedDownload, TrackFeedReader trackFeedReader, IFileHelper fileHelper, IEventAdapter logEvent)
		{
			_feedDownload = feedDownload;
			_trackFeedReader = trackFeedReader;
			_fileHelper = fileHelper;
			_logEvent = logEvent;
		}

		public FluentFeedMunch WithConfig(FeedMunchConfig config)
		{
			Config = config;
			return this;
		}

		public void Invoke(Feed feed)
		{
			if (_feedDownload.FeedAlreadyExists(feed))
			{
				FilterFeedAndWrite(feed);
			}
			else
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();

				_logEvent.Info(string.Format("Downloading to {0}", _feedDownload.CurrentFileName));

				var saveLocally = _feedDownload.SaveLocally(feed);

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
		}

		private void FilterFeedAndWrite(Feed feed)
		{
			var rows = ReadAllRows(feed);

			var filteredFeed = FilterRows(rows);

			var outputFeedPath = GenerateOutputFeedLocation();

			_logEvent.Info(string.Format("Writing filtered feed out to {0}", outputFeedPath));
			var timeFilteredFeedWrite = TimerHelper.TimeMe(() => TryOutputFIlteredFeed(outputFeedPath, filteredFeed));
			_logEvent.Info(string.Format("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds));
		}

		private static void TryOutputFIlteredFeed(string outputFeedPath, IEnumerable<Track> filteredFeed)
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

		private string GenerateOutputFeedLocation()
		{
			_fileHelper.GetOrCreateFeedsFolder();
			var outputFolder = _fileHelper.GetOrCreateDirectoryAtRoot("feeds/output");
			var outputFeedPath = Path.Combine(outputFolder, "testfile.tmp");
			return outputFeedPath;
		}

		private IEnumerable<Track> ReadAllRows(Feed feed)
		{
			// TODO - trial hard loading this into ram before processing as this may be bottlenecked by disk IO
			// TODO - Specify row numbers
			_logEvent.Info("Reading data into list");
			return _trackFeedReader.ReadIntoList(feed);
		}

		private IEnumerable<Track> FilterRows(IEnumerable<Track> rows)
		{
			// TODO - Needs to be customisable via a config value - also, this is typesafe we need to be able to produce filter based on string (dynamic?)
			return rows.Where(track => track.streamingReleaseDate < DateTime.Now);
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