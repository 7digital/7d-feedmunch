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
			var request = context.Request;
			var response = context.Response;

			var feedMunchConfig = context.Request.Url.ToFeedMunchConfig();

			if (request.Params["test"] == "true")
			{
				response.ContentType = "text/plain";
				response.StatusCode = (int) HttpStatusCode.OK;
				response.Write(String.Format("I am {0} {1}", feedMunchConfig.Catalog.ToString().ToLower(), feedMunchConfig.Feed.ToString().ToLower()));
			}
			else
			{
				using (var gzip = new GZipStream(context.Response.OutputStream, CompressionMode.Compress))
				{
					try
					{
						response.BufferOutput = false;
						response.ContentType = "application/x-gzip";
						response.StatusCode = (int)HttpStatusCode.OK;
						response.Headers.Set("Content-Encoding", "gzip");
						response.Headers.Set("Content-disposition", "attachment; filename=\"20140203-GB-artist-full-feed-filtered.gz\"");

						FeedMuncher.IOC.StructureMap
							.FeedMunch.Download
							.WithConfig(feedMunchConfig)
							.InvokeAndWriteTo(gzip);
					}
					catch (ArgumentException ex)
					{
						response.ContentType = "text/html";
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						response.Write(ex.Message);
						response.Close();
						return;
					}
					response.Flush();
				}
			}
		}
	}
}