using System.Threading.Tasks;

namespace SevenDigital.Api.FeedReader.Http
{
	public interface IWebClientWrapper
	{
		Task DownloadFile(string address, string fileName);
		Task ResumeDownloadFile(string address, string fileName);
	}
}