using NUnit.Framework;
using SevenDigital.Api.FeedReader.Feeds;

namespace SevenDigital.Api.FeedReader.Unit.Tests.Feeds
{
	[TestFixture]
	public class FeedReadingTests
	{
	}

	public static class TestFeed
	{
		public static string BasicTrackFeed()
		{
			return "trackId,title,version,type,isrc,explicitContent,trackNumber,discNumber,artistId,artistAppearsAs,releaseId,duration,formats,price,rrp,url,popularity,streamingReleaseDate " +
			"1660,Snowed Under,,Audio,GBAAN0300721,false,2,1,1,Keane,135,228,\"17,55,56,26\",1.69,1.69,http://www.zdigital.com.au/artist/keane/release/somewhere-only-we-know-enhanced?h=02,0.36,2004-05-17T00:00:00Z" +
			"1661,Walnut Tree,,Audio,GBAAN0300720,false,3,1,1,Keane,135,220,\"17,55,56,26\",1.69,1.69,http://www.zdigital.com.au/artist/keane/release/somewhere-only-we-know-enhanced?h=03,0.35,2004-05-17T00:00:00Z";
		}

		public static string BasicArtistFeed()
		{
			return "";
		}

		public static string BasicReleaseFeed()
		{
			return "";
		}
	}

	[TestFixture]
	public class FeedTests
	{
		[Test]
		public void ArtistFull()
		{
			var feed = AvailableFeeds.ArtistFull;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void ArtistIncremental()
		{
			var feed = AvailableFeeds.ArtistIncremental;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Updates));
		}

		[Test]
		public void TrackFull()
		{
			var feed = AvailableFeeds.TrackFull;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void TrackIncremental()
		{
			var feed = AvailableFeeds.TrackIncremental;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Updates));
		}

		[Test]
		public void ReleaseFull()
		{
			var feed = AvailableFeeds.ReleaseFull;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void ReleaseIncremental()
		{
			var feed = AvailableFeeds.ReleaseIncremental;

			Assert.That(feed.CatalogueType, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feed.FeedType, Is.EqualTo(FeedType.Updates));
		}
	}
}
