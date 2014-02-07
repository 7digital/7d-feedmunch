using System;
using System.Collections;
using System.IO.Compression;
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
				var feedMunchConfig = new FeedMunchConfig
				{
					Catalog = FeedCatalogueType.Artist,
					Country = "GB",
					Feed = FeedType.Updates,
					Filter = "action=U"
				};

				using (var gzip = new GZipStream(context.Response.OutputStream, CompressionMode.Compress))
				{
					FeedMuncher.IOC.StructureMap
						.FeedMunch.Download
						.WithConfig(feedMunchConfig)
						.InvokeAndWriteTo(gzip);

					response.ContentType = "application/x-gzip";
					response.StatusCode = (int)HttpStatusCode.OK;
					response.Headers.Set("Content-disposition", "attachment; filename=\"20140203-GB-artist-full-feed-filtered.gz\"");
				}
			}
		}
	}
}