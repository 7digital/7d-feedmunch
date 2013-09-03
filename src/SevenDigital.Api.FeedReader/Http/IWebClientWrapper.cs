using System.IO;
using System.Net;

namespace SevenDigital.Api.FeedReader.Http
{
	public interface IWebClientWrapper
	{
		void DownloadFile(string address, string fileName);
		HttpStatusCode Head(string url);
		Stream GetStream(string url);
		string GetString(string url);
	}
}