using System;
using System.IO.Compression;
using System.Net;
using System.Web;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : HttpHandlerBase, IHttpHandler
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			var response = context.Response;
			var feedMunchConfig = context.Request.Url.ToFeedMunchConfig(); // TODO - need to know filename of original request, so should have response details stored somewhere.
			var contentDisposition = string.Format("attachment; filename=\"20140203-{0}-{1}-{2}-feed-filtered.gz\"", feedMunchConfig.Country, feedMunchConfig.Catalog.ToString().ToLower(), feedMunchConfig.Feed.ToString().ToLower());
			
			using (var gzip = new GZipStream(context.Response.OutputStream, CompressionMode.Compress))
			{
				try
				{
					response.BufferOutput = false;
					response.ContentType = "application/x-gzip";
					response.StatusCode = (int)HttpStatusCode.OK;
					response.Headers.Set("Content-Encoding", "gzip");
					response.Headers.Set("Content-disposition", contentDisposition);
					if(context.Request.HttpMethod != "HEAD")
					{
						FeedMuncher.IOC.StructureMap
							.FeedMunch.Download
							.WithConfig(feedMunchConfig)
							.InvokeAndWriteTo(gzip);
					}
				}
				catch (ArgumentException ex)
				{
					response.ContentType = "text/html";
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					response.Write(ex.Message);
				}
			}
		}
	}
}