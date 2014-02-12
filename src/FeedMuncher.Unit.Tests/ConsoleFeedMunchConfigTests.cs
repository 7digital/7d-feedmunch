using System;
using NUnit.Framework;
using SevenDigital.FeedMunch;

namespace FeedMuncher.Unit.Tests
{
	[TestFixture]
	public class ConsoleFeedMunchConfigTests
	{
		[Test]
		public void Default_output_filename()
		{
			var now = DateTime.Now.ToString("yyyyMMdd");

			var feedMunchConfig = new ConsoleFeedMunchConfig();
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./"+now+"-GB-artist-full-feed"));
		}

		[Test]
		public void Track_full_output_filename()
		{
			var now = DateTime.Now.ToString("yyyyMMdd");

			var feedMunchConfig = new ConsoleFeedMunchConfig { Feed = FeedType.Full, Catalog = FeedCatalogueType.Track};
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./" + now + "-GB-track-full-feed"));
		}

		[Test]
		public void Track_update_output_filename()
		{
			var now = DateTime.Now.ToString("yyyyMMdd");

			var feedMunchConfig = new ConsoleFeedMunchConfig { Feed = FeedType.Updates, Catalog = FeedCatalogueType.Track };
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./" + now + "-GB-track-updates-feed"));
		}

		[Test]
		public void country_output_filename()
		{
			var now = DateTime.Now.ToString("yyyyMMdd");

			var feedMunchConfig = new ConsoleFeedMunchConfig { Country = "US" };
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./" + now + "-US-artist-full-feed"));
		}

		[Test]
		public void filtered_output_filename()
		{
			var now = DateTime.Now.ToString("yyyyMMdd");

			var feedMunchConfig = new ConsoleFeedMunchConfig { Filter = "name=Keane" };
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./" + now + "-GB-artist-full-feed-filtered"));
		}
	}
}
