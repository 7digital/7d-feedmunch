using SevenDigital.Api.FeedReader.Feeds.Artist;
using SevenDigital.Api.FeedReader.Feeds.Release;
using SevenDigital.Api.FeedReader.Feeds.Track;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public static class AvailableFeeds
	{
		public static Feed ArtistFull { get { return new ArtistFullFeed(); } }
		public static Feed ArtistIncremental { get { return new ArtistIncrementalFeed(); } }
		public static Feed TrackFull { get { return new TrackFullFeed(); } }
		public static Feed TrackIncremental { get { return new TrackIncrementalFeed(); } }
		public static Feed ReleaseFull { get { return new ReleaseFullFeed(); } }
		public static Feed ReleaseIncremental { get { return new ReleaseIncrementalFeed(); } }
	}
}