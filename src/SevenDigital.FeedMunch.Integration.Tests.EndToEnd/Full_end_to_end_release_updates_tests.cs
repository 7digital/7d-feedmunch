﻿using System;
using System.IO;
using NUnit.Framework;
using SevenDigital.FeedMunch.Feeds.Schema;
using SevenDigital.FeedMunch.IOC.StructureMap;

namespace SevenDigital.FeedMunch.Integration.Tests.EndToEnd
{
	[TestFixture]
	[Category("Smoke")]
	public class Full_end_to_end_release_updates_tests
	{
		private const string OUTPUT_FILE = "releaseUpdatesTest";

		[Test]
		public void Can_filter_action_on_the_fly()
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = new FeedMunchConfig
			{
				Catalog = FeedCatalogueType.Release,
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

				AssertFiltering.IsAsExpected<ReleaseIncremental>(ms, x => x.action == "U");
			}
		}
	}
}