using System;
using System.Collections;
using System.Net;
using System.Web;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : HttpHandlerBase, IHttpHandler
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			var response = context.Response;
			var segments = new Stack(context.Request.Url.Segments);

			var feedType = ((string)segments.Pop()).TrimEnd('/');
			var catalogType = ((string)segments.Pop()).TrimEnd('/');

			response.ContentType = "text/plain";
			response.StatusCode = (int)HttpStatusCode.OK;
			response.Write(String.Format("I am {0} {1}", catalogType, feedType));
		}
	}

	public abstract class HttpHandlerBase
	{
		public void ProcessRequest(HttpContext context)
		{
			ProcessRequest(new HttpContextWrapper(context));
		}

		public abstract void ProcessRequest(HttpContextBase context);

		public bool IsReusable { get { return true; } }
	}
}