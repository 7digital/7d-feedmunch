using System;
using FeedMuncher.IOC.StructureMap;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace FeedMuncher.TrackLicensorId
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunch.Configure.FromConsoleArgs(args);
			feedMunchConfig.Catalog = FeedCatalogueType.Track;
			feedMunchConfig.Filter = "licensorID=" + feedMunchConfig.Filter;
			
			// This is currently hard coded to track, was supposed to be infererred from CatalogType - not sure if this is possible
			var fluentFeedMunch = FeedMunch.Download.WithConfig(feedMunchConfig);

			if (feedMunchConfig.Feed == FeedType.Updates)
			{
				fluentFeedMunch.Invoke<TrackIncremental>();
			}
			else
			{
				fluentFeedMunch.Invoke<Track>();
			}

			Console.Read();
		}
	}
}
