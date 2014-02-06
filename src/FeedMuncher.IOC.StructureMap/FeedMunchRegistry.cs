using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Configuration;
using SevenDigital.FeedMunch;
using StructureMap.Configuration.DSL;

namespace FeedMuncher.IOC.StructureMap
{
	public class FeedMunchRegistry : Registry
	{
		public FeedMunchRegistry()
		{
			Scan(x =>
			{
				x.AssemblyContainingType<Feed>();
				x.AssemblyContainingType<FluentFeedMunch>();
				x.WithDefaultConventions();
				x.SingleImplementationsOfInterface();
			});

			var oAuthConsumerCreds = OAuthConsumerCreds.GenerateFromFile("credentials.txt");
			For<OAuthConsumerCreds>().Use(oAuthConsumerCreds);
			For<IFileHelper>().Use<FeedsFileHelper>().Ctor<string>("outputFolder").Is("output");
		}
	}
}