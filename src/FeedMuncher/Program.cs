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
			//	Feed = FeedType.Full,
			//	Filter = "",
			//	Limit = 100,
			//	Shop = 34
			//};

			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.Invoke(); // TODO return filepath?

			Console.Read();
		}
	}
}
