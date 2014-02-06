using System.IO;
using System.IO.Compression;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds;

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
			_logLog.Info(FeedDescription.ToString());
			return this;
		}

		/// <summary>
		/// Default behaviour, writes output of filtered feed to a gzipped file 
		/// </summary>
		public void InvokeAndWriteToGzippedFile()
		{
			var generateOutputFeedLocation = _fileHelper.GenerateOutputFeedLocation(Config.Output);
			
			using (var output = File.Create(generateOutputFeedLocation))
			{
				using (var gzipOut = new GZipStream(output, CompressionMode.Compress))
				{
					InvokeAndWriteTo(gzipOut);
				}
			}
			TryChangeExtension(generateOutputFeedLocation, ".tmp", ".gz");
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

			var filterStream = TimerHelper.TimeMe(() => Filter.ApplyToStream(decompressedStream, outputStream));

			_logLog.Info(string.Format("Took {0} milliseconds to output filtered feed", filterStream.ElapsedMilliseconds));
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
			FeedDescription = new Feed(config.Feed, config.Catalog) { Country = config.Country };
			Filter = new Filter(config.Filter);
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