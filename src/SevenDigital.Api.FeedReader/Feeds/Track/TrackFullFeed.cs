using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackFullFeed : Feed
	{
		public override FeedCatalogueType GetCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Track;
		}

		public override FeedType GetFeedType()
		{
			return FeedReader.FeedType.Full;
		}
	}
}