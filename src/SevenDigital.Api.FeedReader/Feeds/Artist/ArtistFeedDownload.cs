using System.IO;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IWebClientWrapper _webClient;
		private readonly Feed _artistFeed;

		public ArtistFeedDownload(IFeedsUrlCreator feedsUrlCreator, IWebClientWrapper webClient, Feed artistFeed)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_webClient = webClient;
			_artistFeed = artistFeed;
		}

		public void SaveLocally()
		{
			if (FeedAlreadyExists()) return;

			var signedFeedsUrl = _feedsUrlCreator.SignUrlForLatestArtistFeed(FeedType.Full, "GB");

			_webClient.DownloadFile(signedFeedsUrl, _artistFeed.GetLatest());
		}

		public bool FeedAlreadyExists()
		{
			if (!File.Exists(_artistFeed.GetLatest()))
				return false;

			return true;
		}
	}
}