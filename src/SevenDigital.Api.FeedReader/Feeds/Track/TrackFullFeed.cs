using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackFullFeed : Feed
	{
		public override FeedCatalogueType FeedCatalogueType()
		{
			return FeedReader.FeedCatalogueType.Track;
		}

		public override FeedType FeedType()
		{
			return FeedReader.FeedType.Full;
		}
	}
}