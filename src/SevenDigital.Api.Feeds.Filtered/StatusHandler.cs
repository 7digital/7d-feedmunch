using System;
using System.Web;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class StatusHandler : HttpHandlerBase
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			context.Response.Write(DateTime.Now);
			context.Response.Write(Environment.NewLine);
		}
	}
}