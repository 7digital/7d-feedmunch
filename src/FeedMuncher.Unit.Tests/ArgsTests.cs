using NUnit.Framework;
using SevenDigital.Api.FeedReader;

namespace FeedMuncher.Unit.Tests
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
				"/country", "GB"
			};

			var feedMunchConfig = FeedMunchArgumentAdapter.FromConsoleArgs(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Artist));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo("licensorId != 1"));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
		}

		[Test]
		public void Incremental_Track_feed_for_country_US_with_no_filter()
		{
			var args = new[]
			{
				"/feed", "updates", 
				"/catalog", "track", 
				"/filter", "", 
				"/output", "./blah", 
				"/country", "US"
			};

			var feedMunchConfig = FeedMunchArgumentAdapter.FromConsoleArgs(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Track));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Updates));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo(""));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("US"));
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
				"/cOuNtry", "GB"
			};
			
			var feedMunchConfig = FeedMunchArgumentAdapter.FromConsoleArgs(args);

			Assert.That(feedMunchConfig.Catalog, Is.EqualTo(FeedCatalogueType.Release));
			Assert.That(feedMunchConfig.Feed, Is.EqualTo(FeedType.Full));
			Assert.That(feedMunchConfig.Filter, Is.EqualTo("licensorId != 1"));
			Assert.That(feedMunchConfig.Output, Is.EqualTo("./blah"));
			Assert.That(feedMunchConfig.Country, Is.EqualTo("GB"));
		}
	}
}