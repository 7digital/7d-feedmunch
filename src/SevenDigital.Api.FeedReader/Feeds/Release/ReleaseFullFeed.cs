namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseFullFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return feedsDate + "-release-feed.gz";
		}

		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Release;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Full;
		}
	}
}