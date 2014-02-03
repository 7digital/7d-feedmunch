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
				.Invoke();

			Console.Read();
		}
	}
}
