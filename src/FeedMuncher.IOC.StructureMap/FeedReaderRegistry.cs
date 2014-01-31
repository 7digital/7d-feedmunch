using System;
using SevenDigital.Api.FeedReader;
using SevenDigital.Api.FeedReader.Configuration;
using SevenDigital.FeedMunch;
using StructureMap.Configuration.DSL;

namespace FeedMuncher.IOC.StructureMap
{
	public class FeedReaderRegistry : Registry
	{
		public FeedReaderRegistry()
		{
			Scan(x =>
			{
				x.AssemblyContainingType<Feed>();
				x.AssemblyContainingType<FluentFeedMunch>();
				x.WithDefaultConventions();
				x.SingleImplementationsOfInterface();
			});
			
			For<OAuthConsumerCreds>().Use(new OAuthConsumerCreds("7dwwyz5uxp56", "5g62wtq9znyffsmm"));
			For<IFileHelper>().Use<FeedsFileHelper>().Ctor<string>("feedsFolder").Is("feeds");
		}
	}
}