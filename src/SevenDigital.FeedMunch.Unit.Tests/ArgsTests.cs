using NUnit.Framework;
using SevenDigital.Api.FeedReader;

namespace SevenDigital.FeedMunch.Unit.Tests
{
	[TestFixture]
	public class ArgsTests
	{
		[Test]
		public void Full_artist_feed_for_shop_34_with_licensorId_1_filtered_out()
		{
			var args = new []
			{
				"/feed", "Full", 
				"/catalog", "artist", 
				"/filter", "licensorId != 1", 
				"/output", "./blah", 
				"/shop", "34"
			};

			var feedMunchArgumentAdapter = new FeedMunchArgumentAdapter();
			var feedMunchConfig = feedMunchArgumentAdapter.ToConfig(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo("licensorId != 1"));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Shop, Is.EqualTo(34));
		}

		[Test]
		public void Incremental_Track_feed_for_shop_1106_with_no_filter()
		{
			var args = new[]
			{
				"/feed", "incremental", 
				"/catalog", "track", 
				"/filter", "", 
				"/output", "./blah", 
				"/shop", "1106"
			};

			var feedMunchArgumentAdapter = new FeedMunchArgumentAdapter();
			var feedMunchConfig = feedMunchArgumentAdapter.ToConfig(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Incremental));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Shop, Is.EqualTo(1106));
		}

		[Test]
		public void Args_are_case_insenstitive()
		{
			var args = new[]
			{
				"/feeEd", "full", 
				"/caTalog", "Release", 
				"/fiLter", "licensorId != 1", 
				"/ouTput", "./blah", 
				"/sHop", "34"
			};
			
			var feedMunchArgumentAdapter = new FeedMunchArgumentAdapter();
			var feedMunchConfig = feedMunchArgumentAdapter.ToConfig(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo("licensorId != 1"));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Shop, Is.EqualTo(34));
		}

	}
}
