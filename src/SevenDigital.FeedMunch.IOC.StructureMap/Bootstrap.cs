using StructureMap;

namespace SevenDigital.FeedMunch.IOC.StructureMap
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
		public static FluentFeedMunch Download
		{
			get { return ObjectFactory.GetInstance<FluentFeedMunch>(); }
		}
	}
}