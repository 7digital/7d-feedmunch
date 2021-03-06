﻿using System;
using System.IO;
using NUnit.Framework;
using SevenDigital.FeedMunch.Feeds.Schema;
using SevenDigital.FeedMunch.IOC.StructureMap;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Ignore("The artist tests are causing issues because the feeds aren't always there, so ignoring for now. Maybe pick up as work at a later date to allo console app to check for latest feed if current is 404ing, or wait until this functionality is offered by the feeds-api")]
	[Category("Smoke")]
	public class Full_end_to_end_artist_updates_tests
	{
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
				Date = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now.AddDays(-1), FeedType.Updates)
			};

			using (var ms = new MemoryStream())
			{
				IOC.StructureMap
						.FeedMunch.Download
						.WithConfig(feedMunchConfig)
						.InvokeAndWriteTo(ms);

				ms.Position = 0;

				AssertFiltering.IsAsExpected<ArtistIncremental>(ms, x => x.action == "U");
			}
		}
	}
}