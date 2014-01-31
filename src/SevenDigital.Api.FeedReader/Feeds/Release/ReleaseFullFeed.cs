namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseFullFeed : Feed
	{
		public override FeedCatalogueType GetCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Release;
		}

		public override FeedType GetFeedType()
		{
			return FeedReader.FeedType.Full;
		}
	}
}