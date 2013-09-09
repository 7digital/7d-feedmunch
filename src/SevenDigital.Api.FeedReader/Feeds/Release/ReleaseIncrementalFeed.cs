namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseIncrementalFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousIncrementalFeedDate();
			return feedsDate + "-release-inc-feed.gz";
		}

		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Release;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Incremental;
		}
	}
}