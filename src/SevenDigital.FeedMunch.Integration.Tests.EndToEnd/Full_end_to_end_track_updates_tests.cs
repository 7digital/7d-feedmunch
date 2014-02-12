using System;
using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.FeedMunch.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Category("Smoke")]
	public class Full_end_to_end_track_updates_tests
	{
		private const string OUTPUT_FILE = "trackUpdatesTest";

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
				Date = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now.AddDays(-1), FeedType.Updates)
			};

			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<TrackIncremental>(ms, x => x.version == "Album Version");
			}
		}
	}
}