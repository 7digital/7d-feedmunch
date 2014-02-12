using System;
using System.IO.Compression;
using System.Net;
using System.Web;
using SevenDigital.Api.FeedReader;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : HttpHandlerBase
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			var response = context.Response;
			var feedMunchConfig = context.Request.Url.ToFeedMunchConfig(); 
			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, feedMunchConfig.Feed);

			var contentDisposition = string.Format("attachment; filename=\"{0}_{1}_{2}_{3}-filtered.gz\"", feedMunchConfig.Country, feedMunchConfig.Catalog.ToString().ToLower(), feedMunchConfig.Feed.ToString().ToLower(), currentFeedDate);
			
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