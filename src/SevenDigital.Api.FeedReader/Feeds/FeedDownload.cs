using System.IO;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class FeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IWebClientWrapper _webClient;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator, IWebClientWrapper webClient)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_webClient = webClient;
		}

		public void SaveLocally(Feed suppliedFeed)
		{
			if (FeedAlreadyExists(suppliedFeed)) return;

			var signedFeedsUrl = _feedsUrlCreator.SignUrlForLatestArtistFeed(FeedType.Full, "GB");

			_webClient.DownloadFile(signedFeedsUrl, suppliedFeed.GetLatest());
		}

		public bool FeedAlreadyExists(Feed suppliedFeed)
		{
			if (!File.Exists(suppliedFeed.GetLatest()))
				return false;

			return true;
		}
	}
}