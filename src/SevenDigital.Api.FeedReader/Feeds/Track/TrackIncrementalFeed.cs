namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackIncrementalFeed : Feed
	{
		public override FeedCatalogueType GetCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Track;
		}

		public override FeedType GetFeedType()
		{
			return FeedReader.FeedType.Incremental;
		}
	}
}