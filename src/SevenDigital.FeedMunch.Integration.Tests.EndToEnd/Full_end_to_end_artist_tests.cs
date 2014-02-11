using System;
using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Category("Smoke")]
	public class Full_end_to_end_artist_tests
	{
		private const string OUTPUT_FILE = "artistFullTest";
		private const string EXPECTED_OUTPUT_FILE = "output/" + OUTPUT_FILE + ".gz";

		[Test]
		public void Can_filter_action_on_the_fly()
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = new FeedMunchConfig
			{
				Catalog = FeedCatalogueType.Artist,
				Country = "GB",
				Feed = FeedType.Full,
				Filter = "name=Interpol,U2",
				Output = OUTPUT_FILE,
				Date = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now.AddDays(-1), FeedType.Full)
			};

			FeedMuncher.IOC.StructureMap
			           .FeedMunch.Download
			           .WithConfig(feedMunchConfig)
			           .InvokeAndWriteToGzippedFile();

			Assert.That(File.Exists(EXPECTED_OUTPUT_FILE));

			AssertFiltering.IsAsExpected<Artist>(EXPECTED_OUTPUT_FILE, x => x.name == "Interpol" || x.name == "U2");
		}
	}
}