using System;
using FeedMuncher.IOC.StructureMap;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunch.Configure.FromConsoleArgs(args);

			//var feedMunchConfig = new FeedMunchConfig
			//{
			//	Catalog = FeedCatalogueType.Track,
			//	Existing = @"H:\New folder\00034_track_full_20140130_licensor.gz",
			//	Limit = 100,
			//	Filter="licensorID=1"
			//};

			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.Invoke(); // TODO return filepath?

			Console.Read();
		}
	}
}
