using NUnit.Framework;
using SevenDigital.Api.FeedReader.Feeds;

namespace SevenDigital.Api.FeedReader.Unit.Tests.Feeds
{
	[TestFixture]
	public class FeedTests
	{
		[Test]
		public void ArtistFull()
		{
			var feed = AvailableFeeds.ArtistFull;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void ArtistIncremental()
		{
			var feed = AvailableFeeds.ArtistIncremental;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Incremental));
		}

		[Test]
		public void TrackFull()
		{
			var feed = AvailableFeeds.TrackFull;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void TrackIncremental()
		{
			var feed = AvailableFeeds.TrackIncremental;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Incremental));
		}

		[Test]
		public void ReleaseFull()
		{
			var feed = AvailableFeeds.ReleaseFull;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Full));
		}

		[Test]
		public void ReleaseIncremental()
		{
			var feed = AvailableFeeds.ReleaseIncremental;

			Assert.That(feed.FeedCatalogueType(), Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feed.FeedType(), Is.EqualTo(FeedType.Incremental));
		}
	}
}
