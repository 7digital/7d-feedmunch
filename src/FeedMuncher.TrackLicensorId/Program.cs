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
			
			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.InvokeAndWriteToGzippedFile();


			Console.Read();
		}
	}
}
