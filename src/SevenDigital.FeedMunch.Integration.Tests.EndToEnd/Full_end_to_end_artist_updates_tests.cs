﻿using System;
using System.IO;
using FeedMuncher.IOC.StructureMap;
using NUnit.Framework;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Category("Smoke")]
	public class Full_end_to_end_artist_updates_tests
	{
		private const string OUTPUT_FILE = "artistUpdatesTest";
		private const string EXPECTED_OUTPUT_FILE = "output/" + OUTPUT_FILE + ".gz";

		[Test]
		public void Can_filter_action_on_the_fly()
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = new FeedMunchConfig
			{
				Catalog = FeedCatalogueType.Artist,
				Country = "GB",
				Feed = FeedType.Updates,
				Filter = "action=U",
				Output = OUTPUT_FILE,
				Date = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now.AddDays(-1), FeedType.Updates)
			};

			using (var ms = new MemoryStream())
			{
				FeedMuncher.IOC.StructureMap
						   .FeedMunch.Download
						   .WithConfig(feedMunchConfig)
						   .InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<ArtistIncremental>(ms, x => x.action == "U");
			}
		}
	}
}