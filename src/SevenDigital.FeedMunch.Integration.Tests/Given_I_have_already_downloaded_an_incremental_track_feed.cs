using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests
{
	[TestFixture]
	public class Given_I_have_already_downloaded_an_incremental_track_feed
	{
		private FeedMunchConfig _feedMunchConfig;

		[TestFixtureSetUp]
		public void Setup()
		{
			Bootstrap.ConfigureDependencies();

			_feedMunchConfig = new FeedMunchConfig
			{
				Feed = FeedType.Updates,
				Catalog = FeedCatalogueType.Track,
				Existing = @"Samples/track/1000-line-track-updt-feed.gz"
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

				AssertFiltering.IsAsExpected<TrackIncremental>(ms, x => x.licensorID == 1);
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

				AssertFiltering.IsAsExpected<TrackIncremental>(ms, x => x.licensorID == 1 || x.licensorID == 2);
			}
		}

		[Test]
		public void Can_filter_feed_by_action()
		{
			_feedMunchConfig.Filter = "action=I,U";

			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(_feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<TrackIncremental>(ms, x => x.action == "U" || x.action == "I");
			}
		}
	}
}