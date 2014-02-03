using System;
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

		public FeedMunchConfig Config { get; set; }

		public FluentFeedMunch(IFeedDownload feedDownload, TrackFeedReader trackFeedReader, IFileHelper fileHelper, IEventAdapter logEvent)
		{
			_feedDownload = feedDownload;
			_trackFeedReader = trackFeedReader;
			_fileHelper = fileHelper;
			_logEvent = logEvent;
		}

		public void DoTheWholeThing(Feed feed)
		{
			// TODO - need to be able to point at a file on a separate server for int testing - see ArtGrab project
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
			// Read rows from stream
			// TODO - trial hard loading this into ram before processing as this may be bottlenecked by disk IO
			_logEvent.Info("Reading data into list");
			var rows = _trackFeedReader.ReadIntoList(feed);

			// TODO - Needs to be customisable via a config value - also, this is typesafe we need to be able to produce filter based on string (dynamic?)
			var filteredFeed = rows.Where(track => track.streamingReleaseDate < DateTime.Now);

			// TODO - output path needs to be a config value
			_fileHelper.GetOrCreateFeedsFolder();
			var outputFolder = _fileHelper.GetOrCreateDirectoryAtRoot("feeds/output");
			var outputFeedPath = Path.Combine(outputFolder, "testfile.tmp");

			_logEvent.Info(string.Format("Writing filtered feed out to {0}", outputFeedPath));
			var timeFilteredFeedWrite = TimerHelper.TimeMe(() =>
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
					Console.WriteLine( ex.ToString());
				}
			});
			_logEvent.Info(string.Format("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds));
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