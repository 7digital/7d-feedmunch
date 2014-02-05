using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds;
using CsvSerializer = ServiceStack.Text.CsvSerializer;

namespace SevenDigital.FeedMunch
{
	public class FluentFeedMunch : IFluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly IFeedUnpacker _feedUnpacker;
		private readonly IFileHelper _fileHelper;
		private readonly ILogAdapter _logLog;

		public FeedMunchConfig Config { get; private set; }
		public Filter Filter { get; private set; }
		public Feed FeedDescription { get; private set; }

		public FluentFeedMunch(IFeedDownload feedDownload, IFeedUnpacker feedUnpacker, IFileHelper fileHelper, ILogAdapter logLog)
		{
			_feedDownload = feedDownload;
			_feedUnpacker = feedUnpacker;
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

		public void Invoke()
		{
			var generateOutputFeedLocation = _fileHelper.GenerateOutputFeedLocation(Config.Output);

			if (!string.IsNullOrEmpty(Config.Existing))
			{
				FeedDescription.ExistingPath = Config.Existing;
				FilterFeedAndWrite(generateOutputFeedLocation, FeedDescription);
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

					FilterFeedAndWrite(generateOutputFeedLocation, FeedDescription);
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

		private void FilterFeedAndWrite(string feedOutputLocation, Feed feedDescription)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feedDescription);
			_logLog.Info("Reading data into list");
			using (var sr = new StreamReader(decompressedStream))
			{
				var csvReader = new CsvReader(sr);
				csvReader.Read();
				var headers = csvReader.FieldHeaders;
				var filterField = Array.FindIndex(headers, x => x == Filter.FieldName);

				_logLog.Info(string.Format("Writing filtered feed out to {0}", feedOutputLocation));

				var timeFilteredFeedWrite = TimerHelper.TimeMe(() => OutputFilteredFeed(feedOutputLocation, headers, csvReader, filterField));

				_logLog.Info(string.Format("Took {0} milliseconds to output filtered feed", timeFilteredFeedWrite.ElapsedMilliseconds));
			}
		}

		private void OutputFilteredFeed(string feedOutputLocation, string[] headers, ICsvReader csvReader, int filterFieldIndex)
		{
			using (var output = File.Create(feedOutputLocation))
			{
				using (var gzipOut = new GZipStream(output, CompressionMode.Compress))
				{
					CsvSerializer.SerializeToStream(headers, gzipOut);
					while (csvReader.Read())
					{
						var currentRecord = csvReader.CurrentRecord;

						if (Filter.ShouldPass(currentRecord[filterFieldIndex]))
						{
							CsvSerializer.SerializeToStream(currentRecord, gzipOut);
						}
					}
				}
			}
			TryChangeExtension(feedOutputLocation, ".tmp", ".gz");
			csvReader.Dispose();
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