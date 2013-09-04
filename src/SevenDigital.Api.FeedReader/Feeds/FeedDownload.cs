using System.IO;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class FeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IWebClientWrapper _webClient;
		private readonly Feed _suppliedFeed;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator, IWebClientWrapper webClient, Feed suppliedFeed)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_webClient = webClient;
			_suppliedFeed = suppliedFeed;
		}

		public void SaveLocally()
		{
			if (FeedAlreadyExists()) return;

			var signedFeedsUrl = _feedsUrlCreator.SignUrlForLatestArtistFeed(FeedType.Full, "GB");

			_webClient.DownloadFile(signedFeedsUrl, _suppliedFeed.GetLatest());
		}

		public bool FeedAlreadyExists()
		{
			if (!File.Exists(_suppliedFeed.GetLatest()))
				return false;

			return true;
		}
	}
}