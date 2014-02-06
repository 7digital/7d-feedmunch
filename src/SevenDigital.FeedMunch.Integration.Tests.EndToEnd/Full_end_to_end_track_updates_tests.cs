using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Category("Smoke")]
	public class Full_end_to_end_track_updates_tests
	{
		private const string OUTPUT_FILE = "trackUpdatesTest";
		private const string EXPECTED_OUTPUT_FILE = "output/" + OUTPUT_FILE + ".gz";

		[Test]
		public void Can_filter_version_album_version_on_the_fly()
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = new FeedMunchConfig
			{
				Catalog = FeedCatalogueType.Track,
				Country = "GB",
				Feed = FeedType.Updates,
				Filter = "version=Album Version",
				Output = OUTPUT_FILE
			};

			FeedMuncher.IOC.StructureMap
			           .FeedMunch.Download
			           .WithConfig(feedMunchConfig)
			           .Invoke();

			Assert.That(File.Exists(EXPECTED_OUTPUT_FILE));

			AssertFiltering.IsAsExpected<TrackIncremental>(EXPECTED_OUTPUT_FILE, x => x.version == "Album Version");
		}
	}
}