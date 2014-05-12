using SevenDigital.FeedMunch.Configuration;
using StructureMap.Configuration.DSL;

namespace SevenDigital.FeedMunch.IOC.StructureMap
{
	public class FeedMunchRegistry : Registry
	{
		public FeedMunchRegistry()
		{
			Scan(x =>
			{
				x.AssemblyContainingType<FluentFeedMunch>();
				x.WithDefaultConventions();
				x.SingleImplementationsOfInterface();
			});

			var oAuthConsumerCreds = OAuthConsumerCreds.GenerateFromFile("credentials.txt");
			For<OAuthConsumerCreds>().Use(oAuthConsumerCreds);
		}
	}
}