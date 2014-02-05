namespace SevenDigital.Api.FeedReader.Feeds
{
	public static class AvailableFeeds
	{
		public static Feed ArtistFull { get { return new Feed(FeedType.Full, FeedCatalogueType.Artist); } }
		public static Feed ArtistIncremental { get { return new Feed(FeedType.Updates, FeedCatalogueType.Artist); } }
		public static Feed TrackFull { get { return new Feed(FeedType.Full, FeedCatalogueType.Track); } }
		public static Feed TrackIncremental { get { return new Feed(FeedType.Updates, FeedCatalogueType.Track); } }
		public static Feed ReleaseFull { get { return new Feed(FeedType.Full, FeedCatalogueType.Release); } }
		public static Feed ReleaseIncremental { get { return new Feed(FeedType.Updates, FeedCatalogueType.Release); } }
	}
}