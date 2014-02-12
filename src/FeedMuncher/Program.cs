using FeedMuncher.IOC.StructureMap;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunchArgumentAdapter.FromConsoleArgs(args);

			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.InvokeAndWriteTo(new GzippedFileFeedStreamWriter());
		}
	}
}
