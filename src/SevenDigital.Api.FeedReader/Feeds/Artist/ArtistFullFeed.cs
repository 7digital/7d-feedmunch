using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFullFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return feedsDate + "-artist-feed.gz";
		}

		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Artist;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Full;
		}
	}
}