using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return feedsDate + "-track-feed.gz";
		}

		public override FeedCatalogueType FeedType()
		{
			return FeedCatalogueType.Track;
		}
	}
}