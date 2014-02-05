using System;
using FeedMuncher.IOC.StructureMap;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunch.Configure.FromConsoleArgs(args);
			
			// TODO This would make a good int test
			//var feedMunchConfig = new FeedMunchConfig
			//{
			//	Catalog = FeedCatalogueType.Track,
			//	Existing = @"H:\New folder\00034_track_full_20140130_licensor.gz",
			//	Limit = 100,
			//	Filter = "licensorID=1"
			//};

			// This is currently hard coded to track, was supposed to be infererred from CatalogType - not sure if this is possible
			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.Invoke<Track>(); // TODO return filepath? 
#if DEBUG
			Console.Read();
#endif
		}
	}
}
