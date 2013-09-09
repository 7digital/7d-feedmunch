namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackIncrementalFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousIncrementalFeedDate();
			return feedsDate + "-track-inc-feed.gz";
		}

		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Track;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Incremental;
		}
	}
}