using System;
using System.Collections.Specialized;
using System.Web;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;

namespace SevenDigital.Api.Feeds.Filtered.Unit.Tests
{
	[TestFixture]
	public class UriToFeedMunchConfigMapperTests
	{
		[Test]
		public void Should_map_artist_full()
		{
			var uri = new Uri("http://localhost/artist/full");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_artist_update()
		{
			var uri = new Uri("http://localhost/artist/updates");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Updates));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_release_full()
		{
			var uri = new Uri("http://localhost/release/full");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_release_update()
		{
			var uri = new Uri("http://localhost/release/updates");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Updates));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_track_full()
		{
			var uri = new Uri("http://localhost/track/full");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_track_update()
		{
			var uri = new Uri("http://localhost/track/updates");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Updates));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
		}

		[Test]
		public void Should_map_country_code()
		{
			var uri = new Uri("http://localhost/track/updates?country=US");

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Country, Is.EqualTo("US"));
		}

		[Test]
		public void Should_map_filter()
		{
			const string expected = "licensorID=21";
			var filter = HttpUtility.UrlEncode(expected);
			
			var uri = new Uri("http://localhost/track/updates?filter=" + filter);

			var feedMunchConfig = uri.ToFeedMunchConfig();

			Assert.That(feedMunchConfig.Filter, Is.EqualTo(expected));
		}
	}
}
