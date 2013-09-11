using System.IO;
using System.Net;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public interface IFeedDownload
	{
		void SaveLocally(Feed suppliedFeed);
		bool FeedAlreadyExists(Feed suppliedFeed);
		string CurrentSignedUrl { get; }
	}

	public class FeedDownload : IFeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IWebClientWrapper _webClient;
		private readonly IFileHelper _fileHelper;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator, IWebClientWrapper webClient, IFileHelper fileHelper)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_webClient = webClient;
			_fileHelper = fileHelper;
		}

		public void SaveLocally(Feed suppliedFeed)
		{
			CurrentSignedUrl = _feedsUrlCreator.SignUrlForLatestFeed(suppliedFeed.FeedCatalogueType(), suppliedFeed.FeedType(), suppliedFeed.CountryCode);

			var fileName = _fileHelper.BuildFullFilepath(suppliedFeed);
			_webClient.DownloadFile(CurrentSignedUrl, fileName);
		}

		public bool FeedAlreadyExists(Feed suppliedFeed)
		{
			return _fileHelper.FeedExists(suppliedFeed);
		}

		public string CurrentSignedUrl { get; private set; }
	}
}