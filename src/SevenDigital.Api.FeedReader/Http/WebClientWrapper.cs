using System.IO;
using System.Net;

namespace SevenDigital.Api.FeedReader.Http
{
	public class WebClientWrapper : IWebClientWrapper
	{
		public void DownloadFile(string address, string fileName)
		{
			new WebClient().DownloadFile(address, fileName);
		}

		public HttpStatusCode Head(string url)
		{
			var request = WebRequest.Create(url);
			request.Method = "HEAD";
			try
			{
				using (var response = (HttpWebResponse) request.GetResponse())
				{
					return response.StatusCode;
				}
			}
			catch (WebException ex)
			{
				using (var httpWebResponse = ((HttpWebResponse) ex.Response))
				{
					return httpWebResponse.StatusCode;
				}
			}
		}

		public Stream GetStream(string url)
		{
			var request = WebRequest.Create(url);
			request.Method = "GET";
			var response = (HttpWebResponse) request.GetResponse();
			return response.GetResponseStream();
		}

		public string GetString(string url)
		{
			var request = WebRequest.Create(url);
			request.Method = "GET";

			using (var response = TryGetResponse(request))
			{
				using (var streamReader = new StreamReader(response.GetResponseStream()))
				{
					return streamReader.ReadToEnd();
				}
			}
		}

		private static HttpWebResponse TryGetResponse(WebRequest request)
		{
			try
			{
				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException ex)
			{
				return (HttpWebResponse)ex.Response;
			}
		}
	}
}