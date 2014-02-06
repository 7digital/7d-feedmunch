using System;
using System.Collections;
using System.Net;
using System.Web;
using SevenDigital.Api.FeedReader;
using SevenDigital.FeedMunch;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : HttpHandlerBase, IHttpHandler
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			var request = context.Request;
			var response = context.Response;

			var segments = new Stack(request.Url.Segments);
			var feedType = ((string)segments.Pop()).TrimEnd('/');
			var catalogType = ((string)segments.Pop()).TrimEnd('/');
			
			if (request.Params["test"] == "true")
			{
				response.ContentType = "text/plain";
				response.StatusCode = (int) HttpStatusCode.OK;
				response.Write(String.Format("I am {0} {1}", catalogType, feedType));
			}
			else
			{
				//var feedMunchConfig = new FeedMunchConfig
				//{
				//	Catalog = FeedCatalogueType.Artist,
				//	Country = "GB",
				//	Feed = FeedType.Updates,
				//	Filter = "action=U"
				//};

				//FeedMuncher.IOC.StructureMap
				//	.FeedMunch.Download
				//	.WithConfig(feedMunchConfig)
				//	.InvokeAndWriteTo(context.Response.OutputStream);

				//response.ContentType = "application/x-gzip";
				//response.StatusCode = (int)HttpStatusCode.OK;

				// build config

				// invoke and write to gzip compressed output stream

				// set content-type
				// set content-disposition  Content-Disposition: attachment; filename="same as original but with "-filtered" "
				
				response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
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