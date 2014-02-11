using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public interface IFeedDownload
	{
		string CurrentSignedUrl { get; }
		Task<Stream> DownloadToStream(Feed suppliedFeed);
	}

	public class FeedDownload : IFeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator)
		{
			_feedsUrlCreator = feedsUrlCreator;
		}

		public async Task<Stream> DownloadToStream(Feed suppliedFeed)
		{
			CurrentSignedUrl = _feedsUrlCreator.SignUrlForLatestFeed(suppliedFeed);
			
			var httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
			};
			httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.UserAgent.ToString(), "FeedMunch Feed Client");

			var httpResponseMessage = httpClient.GetAsync(CurrentSignedUrl, HttpCompletionOption.ResponseHeadersRead).Result;
			httpResponseMessage.EnsureSuccessStatusCode();

			return await httpResponseMessage.Content.ReadAsStreamAsync();
		}

		public string CurrentSignedUrl { get; private set; }
	}
}