using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests
{
	[TestFixture]
	public class Given_I_have_already_downloaded_a_feed
	{
		[TestFixtureSetUp]
		public void Setup()
		{
			Bootstrap.ConfigureDependencies();
		}

		[Test]
		public void Can_filter_small_track_Feed_by_licensorId()
		{
			var feedMunchConfig = new FeedMunchConfig
			{
				Feed = FeedType.Full,
				Catalog = FeedCatalogueType.Track,
				Existing = @"Samples/track/1000-line-track-full-feed.gz",
				//Limit = 100,
				Filter = "licensorId=1",
				Output = "trackTest"
			};

			FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.Invoke<Track>();

			Assert.That(File.Exists("output/trackTest.gz"));

			File.Delete("output/trackTest.gz");
		}
	}
}
