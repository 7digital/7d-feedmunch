using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFeed : Feed
	{
		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return feedsDate + "-artist-feed.gz";
		}

		public override FeedCatalogueType FeedType()
		{
			return FeedCatalogueType.Artist;
		}
	}
}