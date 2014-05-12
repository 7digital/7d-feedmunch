using System;
using SevenDigital.FeedMunch.IOC.StructureMap;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrap.ConfigureDependencies();
		}
	}
}