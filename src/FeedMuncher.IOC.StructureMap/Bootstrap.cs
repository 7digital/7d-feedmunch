using SevenDigital.FeedMunch;
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
		public static FluentFeedMunch Fluent()
		{
			return ObjectFactory.GetInstance<FluentFeedMunch>();
		}

		public static FeedMunchArgumentAdapter Arguments()
		{
			return ObjectFactory.GetInstance<FeedMunchArgumentAdapter>();
		}
	}
}