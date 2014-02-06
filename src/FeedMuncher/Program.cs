using FeedMuncher.IOC.StructureMap;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunch.Configure.FromConsoleArgs(args);
			
			FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.Invoke(); // TODO return filepath? 
		}
	}
}
