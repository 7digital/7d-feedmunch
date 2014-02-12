using System;
using System.Globalization;
using System.IO;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds;

namespace SevenDigital.FeedMunch
{
	public class FluentFeedMunch : IFluentFeedMunch
	{
		private readonly IFeedDownload _feedDownload;
		private readonly IFeedUnpacker _feedUnpacker;
		private readonly ILogAdapter _logLog;

		public FeedMunchConfig Config { get; private set; }
		public Filter Filter { get; private set; }
		public Feed FeedDescription { get; private set; }
		
		public FluentFeedMunch(IFeedDownload feedDownload, IFeedUnpacker feedUnpacker, ILogAdapter logLog)
		{
			_feedDownload = feedDownload;
			_feedUnpacker = feedUnpacker;
			_logLog = logLog;

			Init(new FeedMunchConfig());
		}

		public IFluentFeedMunch WithConfig(FeedMunchConfig config)
		{
			Config = config;
			Init(Config);
			_logLog.Info(FeedDescription.ToString());
			return this;
		}
		
		/// <summary>
		/// Writes output of filtered feed to the Stream supplied
		/// </summary>
		/// <param name="outputStream">Any writable stream to which you wish to dump the downloaded feed</param>
		public void InvokeAndWriteTo(Stream outputStream)
		{
			var inputStream = ConfigureInputStream();

			var decompressedStream = _feedUnpacker.GetDecompressedStream(inputStream, FeedDescription);

			_logLog.Info("Reading data into list");

			var filterStreamTimeMeasurement = TimerHelper.TimeMe(() => Filter.ApplyToStream(decompressedStream, outputStream));

			_logLog.Info(string.Format("Took {0} milliseconds to output filtered feed", filterStreamTimeMeasurement.ElapsedMilliseconds));
		}

		private Stream ConfigureInputStream()
		{
			if (!string.IsNullOrEmpty(Config.Existing))
			{
				FeedDescription.ExistingPath = Config.Existing;
				return _feedUnpacker.GetFeedAsFilestream(FeedDescription);
			}

			return  _feedDownload.DownloadToStream(FeedDescription).Result;
		}

		private void Init(FeedMunchConfig config)
		{
			var date = string.IsNullOrEmpty(config.Date)
					? DateTime.Now
					: DateTime.ParseExact(config.Date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);

			FeedDescription = new Feed(config.Feed, config.Catalog, config.Country, date);
			Filter = new Filter(config.Filter);
		}
	}
}