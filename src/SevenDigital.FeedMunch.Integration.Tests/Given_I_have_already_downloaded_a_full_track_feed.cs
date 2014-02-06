using System;
using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests
{
	[TestFixture]
	public class Given_I_have_already_downloaded_a_full_track_feed
	{
		private const string OUTPUT_FILE = "trackTest";
		private const string EXPECTED_OUTPUT_FILE = "output/" + OUTPUT_FILE + ".gz";

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
				Output = OUTPUT_FILE
			};
		}

		[Test]
		public void Can_filter_feed_by_licensorId()
		{
			_feedMunchConfig.Filter = "licensorID=1";

			FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig)
				.InvokeAndWriteToGzippedFile();

			Assert.That(File.Exists(EXPECTED_OUTPUT_FILE));

			AssertFiltering.IsAsExpected<Track>(EXPECTED_OUTPUT_FILE, x => x.licensorID == 1);
		}

		[Test]
		public void Can_filter_feed_by_2_licensorIds()
		{
			_feedMunchConfig.Filter = "licensorID=1,2";

			FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig)
				.InvokeAndWriteToGzippedFile();

			Assert.That(File.Exists(EXPECTED_OUTPUT_FILE));

			AssertFiltering.IsAsExpected<Track>(EXPECTED_OUTPUT_FILE, x => x.licensorID == 1 || x.licensorID == 2);
		}

		[Test]
		public void Can_filter_feed_by_title()
		{
			_feedMunchConfig.Filter = "title=Jingle Bells";

			FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig)
				.InvokeAndWriteToGzippedFile();

			Assert.That(File.Exists(EXPECTED_OUTPUT_FILE));

			AssertFiltering.IsAsExpected<Track>(EXPECTED_OUTPUT_FILE, x => x.title == "Jingle Bells");
		}

		[Test]
		public void Filtering_by_invalid_field()
		{
			_feedMunchConfig.Filter = "jabba=Jingle Bells";

			var feedMunch = FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig);

			var argumentException = Assert.Throws<ArgumentException>(feedMunch.InvokeAndWriteToGzippedFile);

			Assert.That(argumentException.Message, Is.EqualTo("Chosen filter field is not valid: \"jabba\", remember field names are case sensitive"));
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			if (File.Exists(EXPECTED_OUTPUT_FILE))
			{
				File.Delete(EXPECTED_OUTPUT_FILE);
			}
		}
	}
}
