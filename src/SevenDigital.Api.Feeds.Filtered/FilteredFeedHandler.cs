using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using SevenDigital.FeedMunch;

namespace SevenDigital.Api.Feeds.Filtered
{
	public class FilteredFeedHandler : HttpHandlerBase
	{
		public override void ProcessRequest(HttpContextBase context)
		{
			var response = context.Response;
			var request = context.Request;

			var feedMunchConfig = request.Url.ToFeedMunchConfig();
			FeedMunch.IOC.StructureMap
				.FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.InvokeAndWriteTo(new GzippedHttpFeedStreamWriter(request, response));
		}
	}

	public class GzippedHttpFeedStreamWriter : IFeedStreamWriter  
	{
		private readonly HttpRequestBase _request;
		private readonly HttpResponseBase _response;

		public GzippedHttpFeedStreamWriter(HttpRequestBase request, HttpResponseBase response)
		{
			_request = request;
			_response = response;
		}

		public void Write(FeedMunchConfig feedMunchConfig, Action<Stream> writeFeedStream)
		{
			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, feedMunchConfig.Feed);

			var contentDisposition = string.Format("attachment; filename=\"{0}_{1}_{2}_{3}-filtered.gz\"", feedMunchConfig.Country, feedMunchConfig.Catalog.ToString().ToLower(), feedMunchConfig.Feed.ToString().ToLower(), currentFeedDate);

			using (var gzip = new GZipStream(_response.OutputStream, CompressionMode.Compress))
			{
				try
				{
					_response.BufferOutput = false;
					_response.ContentType = "application/x-gzip";
					_response.StatusCode = (int)HttpStatusCode.OK;
					_response.Headers.Set("Content-Encoding", "gzip");
					_response.Headers.Set("Content-disposition", contentDisposition);
					if (_request.HttpMethod != "HEAD")
					{
						writeFeedStream(gzip);
					}
				}
				catch (ArgumentException ex)
				{
					_response.ContentType = "text/html";
					_response.StatusCode = (int)HttpStatusCode.BadRequest;
					_response.Write(ex.Message);
				}
			}
		}
	}

}