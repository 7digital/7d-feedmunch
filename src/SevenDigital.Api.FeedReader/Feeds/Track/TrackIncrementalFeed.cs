namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackIncrementalFeed : Feed
	{
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