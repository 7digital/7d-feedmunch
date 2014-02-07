using NUnit.Framework;
using SevenDigital.Api.FeedReader.Configuration;

namespace SevenDigital.Api.FeedReader.Unit.Tests.Feeds
{
	[TestFixture]
	public class FeedsUrlCreatorTests
	{
		[Test]
		public void Artist_full()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/artist/full?oauth_consumer_key=KEY"));

		}

		[Test]
		public void Artist_updates()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Updates, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/artist/updates?oauth_consumer_key=KEY"));
		}

		[Test]
		public void Release_full()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Release, FeedType.Full, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/release/full?oauth_consumer_key=KEY"));

		}

		[Test]
		public void Release_updates()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Release, FeedType.Updates, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/release/updates?oauth_consumer_key=KEY"));
		}

		[Test]
		public void Track_full()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Track, FeedType.Full, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/track/full?oauth_consumer_key=KEY"));

		}

		[Test]
		public void Track_updates()
		{
			var feedsUrlCreator = new FeedsUrlCreator(new ApiUrl(), new OAuthConsumerCreds("KEY", "SECRET"));

			var signUrlForLatestFeed = feedsUrlCreator.SignUrlForLatestFeed(FeedCatalogueType.Track, FeedType.Updates, "GB");

			Assert.That(signUrlForLatestFeed, Is.StringStarting("http://feeds.api.7digital.com/1.2/feed/track/updates?oauth_consumer_key=KEY"));
		}

	}
}