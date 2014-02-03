using System;
using FeedMuncher.IOC.StructureMap;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();
			
			var argumentAdapter = FeedMunch.Arguments();
			var feedMunchConfig = argumentAdapter.ToConfig(args);

			var feedDownload = FeedMunch.Fluent();
			feedDownload
				.WithConfig(feedMunchConfig)
				.Invoke(); // DO IT - THE WHOLE THING!! (This will be split into a fluent api, just had to move it out of here, was hurting my head!)

			Console.Read();
		}
	}
}
