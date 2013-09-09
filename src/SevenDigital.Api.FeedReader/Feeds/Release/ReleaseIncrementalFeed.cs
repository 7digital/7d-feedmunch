namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseIncrementalFeed : Feed
	{
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