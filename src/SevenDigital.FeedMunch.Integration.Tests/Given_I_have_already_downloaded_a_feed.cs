using System.IO;
using System.IO.Compression;
using System.Linq;
using DeCsv;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests
{
	[TestFixture]
	public class Given_I_have_already_downloaded_a_full_track_feed
	{
		private FeedMunchConfig _feedMunchConfig;

		[TestFixtureSetUp]
		public void Setup()
		{
			Bootstrap.ConfigureDependencies();

			_feedMunchConfig = new FeedMunchConfig
			{
				Feed = FeedType.Full,
				Catalog = FeedCatalogueType.Track,
				Existing = @"Samples/track/1000-line-track-full-feed.gz",
				Output = "trackTest"
			};

		}

		[Test]
		public void Can_filter_feed_by_licensorId()
		{
			_feedMunchConfig.Filter = "licensorID=1";

			FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig)
				.Invoke<Track>();

			Assert.That(File.Exists("output/trackTest.gz"));

			using (var fileStream = File.OpenRead("output/trackTest.gz"))
			{
				using (var gZipStream = new GZipStream(fileStream, CompressionMode.Decompress))
				{
					var deSerialize = CsvDeserialize.DeSerialize<Track>(gZipStream);
					var enumerable = deSerialize.Where(x => x.licensorID != 1);

					Assert.That(enumerable.Count(), Is.EqualTo(0));
				}
			}
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			if (File.Exists("output/trackTest.gz"))
			{
				File.Delete("output/trackTest.gz");
			}
		}
	}
}
