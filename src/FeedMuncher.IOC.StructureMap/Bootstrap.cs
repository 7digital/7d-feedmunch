using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Feeds;
using SevenDigital.Api.FeedReader.Feeds.Track;
using StructureMap;

namespace FeedMuncher.IOC.StructureMap
{
	public static class Bootstrap
	{
		public static void ConfigureDependencies()
		{
			ObjectFactory.Initialize(expression => expression.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.LookForRegistries();
			}));
		}
	}

	public static class FeedMunch
	{
		public static FeedDownload Download()
		{
			return ObjectFactory.GetInstance<FeedDownload>();
		}

		public static FeedUnpacker Unpack()
		{
			return ObjectFactory.GetInstance<FeedUnpacker>();
		}

		public static TrackFeedReader TrackMunch()
		{
			return ObjectFactory.GetInstance<TrackFeedReader>();
		}

		public static IFileHelper File()
		{
			return ObjectFactory.GetInstance<IFileHelper>();
		}
	}
}
