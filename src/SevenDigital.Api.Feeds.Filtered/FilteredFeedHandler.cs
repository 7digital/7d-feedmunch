using System;
using System.Web;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}
		
		public bool IsReusable { get { return true; } }
	}
}