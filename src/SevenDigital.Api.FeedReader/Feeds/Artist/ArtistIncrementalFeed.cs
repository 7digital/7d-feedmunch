namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistIncrementalFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return feedsDate + "-artist-inc-feed.gz";
		}

		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Artist;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Incremental;
		}
	}
}