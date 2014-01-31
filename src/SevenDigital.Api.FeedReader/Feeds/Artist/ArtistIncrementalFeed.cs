namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistIncrementalFeed : Feed
	{
		public override FeedCatalogueType GetCatalogueType()
		{
			return FeedCatalogueType.Artist;
		}

		public override FeedType GetFeedType()
		{
			return FeedType.Incremental;
		}
	}
}