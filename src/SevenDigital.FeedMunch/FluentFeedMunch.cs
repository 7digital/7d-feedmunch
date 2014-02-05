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
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace SevenDigital.FeedMunch
{
	public class FluentFeedMunch : IFluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly GenericFeedReader _genericFeedReader;
		private readonly IFileHelper _fileHelper;
		private readonly ILogAdapter _logLog;

		public FeedMunchConfig Config { get; private set; }
		public Filter Filter { get; private set; }
		public Feed FeedDescription { get; private set; }

		public FluentFeedMunch(IFeedDownload feedDownload, GenericFeedReader genericFeedReader, IFileHelper fileHelper, ILogAdapter logLog)
		{
			_feedDownload = feedDownload;
			_genericFeedReader = genericFeedReader;
			_fileHelper = fileHelper;
			_logLog = logLog;

			Init(new FeedMunchConfig());
		}

		public IFluentFeedMunch WithConfig(FeedMunchConfig config)
		{
			Config = config;
			Init(Config);
			return this;
		}

		public void Invoke<T>()
		{
			var generateOutputFeedLocation = _fileHelper.GenerateOutputFeedLocation(Config.Output);

			if (!string.IsNullOrEmpty(Config.Existing))
			{
				FeedDescription.ExistingPath = Config.Existing;
				FilterFeedAndWrite<T>(generateOutputFeedLocation, FeedDescription);
			}
			else
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();
				DownloadFromFeedsApi(() => 
				{
					stopwatch.Stop();

					_logLog.Info("Finished downloading");
					_logLog.Info(String.Format("Took {0} milliseconds to download", stopwatch.ElapsedMilliseconds));

					FilterFeedAndWrite<T>(generateOutputFeedLocation, FeedDescription);
				});
			}
		}

		private void Init(FeedMunchConfig config)
		{
			FeedDescription = new Feed(config.Feed, config.Catalog) { Country = config.Country };
			_logLog.Info(FeedDescription.ToString());
			Filter = new Filter(config.Filter);
		}

		private void DownloadFromFeedsApi(Action onDownloaded)
		{
			var downloadFromFeedsApiTask = _feedDownload.SaveLocally(FeedDescription);

			_logLog.Info(string.Format("Downloading to {0}", _feedDownload.CurrentFileName));

			downloadFromFeedsApiTask.ContinueWith(task =>
			{
				if (task.Exception != null)
				{
					_logLog.Info("An error occured downloading the feed");
					_logLog.Info(task.Exception.InnerExceptions.First().Message);
					return;
				}
				onDownloaded();
			}, TaskContinuationOptions.LongRunning);
		}

		private void FilterFeedAndWrite<T>(string feedOutputLocation, Feed feedDescription)
		{
			_logLog.Info("Reading data into list");
			var rows = _genericFeedReader.ReadIntoList<T>(feedDescription);
			var filteredFeed = Filter.Filtrate(rows);

			_logLog.Info(string.Format("Writing filtered feed out to {0}", feedOutputLocation));

			var timeFilteredFeedWrite = TimerHelper.TimeMe(() => TryOutputFilteredFeed(feedOutputLocation, filteredFeed));

			_logLog.Info(string.Format("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds));
		}

		private void TryOutputFilteredFeed<T>(string outputFeedPath, IEnumerable<T> filteredFeed)
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
				_logLog.Info(ex.ToString());
				throw;
			}
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