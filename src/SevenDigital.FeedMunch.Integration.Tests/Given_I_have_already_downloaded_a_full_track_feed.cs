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

		private FeedMunchConfig _feedMunchConfig;

		[TestFixtureSetUp]
		public void Setup()
		{
			Bootstrap.ConfigureDependencies();

			_feedMunchConfig = new FeedMunchConfig
			{
				Feed = FeedType.Full,
				Catalog = FeedCatalogueType.Track,
				Existing = @"Samples/track/1000-line-track-full-feed.gz"
			};
		}

		[Test]
		public void Can_filter_feed_by_licensorId()
		{
			_feedMunchConfig.Filter = "licensorID=1";

			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(_feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<Track>(ms, x => x.licensorID == 1);
			}
		}

		[Test]
		public void Can_filter_feed_by_2_licensorIds()
		{
			_feedMunchConfig.Filter = "licensorID=1,2";
			
			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(_feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<Track>(ms, x => x.licensorID == 1 || x.licensorID == 2);
			}
		}

		[Test]
		public void Can_filter_feed_by_title()
		{
			_feedMunchConfig.Filter = "title=Jingle Bells";

			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(_feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<Track>(ms, x => x.title == "Jingle Bells");
			}
		}

		[Test]
		public void Filtering_by_invalid_field()
		{
			_feedMunchConfig.Filter = "jabba=Jingle Bells";

			var feedMunch = FeedMuncher.IOC.StructureMap.FeedMunch.Download
				.WithConfig(_feedMunchConfig);
			using (var ms = new MemoryStream())
			{
				var argumentException = Assert.Throws<ArgumentException>(() => feedMunch.InvokeAndWriteTo(ms));

				Assert.That(argumentException.Message, Is.EqualTo("Chosen filter field is not valid: \"jabba\", remember field names are case sensitive"));
			}
		}
	}
}
