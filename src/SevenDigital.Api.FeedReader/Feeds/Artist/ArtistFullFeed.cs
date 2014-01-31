namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFullFeed : Feed
	{
		public override FeedCatalogueType GetCatalogueType()
		{
			return FeedCatalogueType.Artist;
		}

		public override FeedType GetFeedType()
		{
			return FeedType.Full;
		}
	}
}