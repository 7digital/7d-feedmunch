using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SevenDigital.Api.FeedReader.Feeds;
using StructureMap;

namespace SevenDigital.Api.FeedReader.Integration.Tests
{
	[TestFixture]
	public class End_to_end
	{
		[Test]
		public void TestName()
		{
			Bootstrap.ConfigureDependencies();
			var feedDownload = ObjectFactory.GetInstance<FeedDownload>();
		}
	}

	public static class Bootstrap
	{
		public static void ConfigureDependencies()
		{
			ObjectFactory.Initialize(expression => expression.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.LookForRegistries();
				scanner.WithDefaultConventions();
				scanner.AssemblyContainingType<Feed>();
			}));
		}
	}
}
