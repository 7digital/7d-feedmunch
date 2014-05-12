using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SevenDigital.FeedMunch.Feeds
{
	public interface IFeedDownload
	{
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
			var currentSignedUrl = _feedsUrlCreator.SignUrlForFeed(suppliedFeed);
			
			var httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
			};
			httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.UserAgent.ToString(), "FeedMunch Feed Client");

			var httpResponseMessage = await httpClient.GetAsync(currentSignedUrl, HttpCompletionOption.ResponseHeadersRead);
			httpResponseMessage.EnsureSuccessStatusCode();

			return await httpResponseMessage.Content.ReadAsStreamAsync();
		}
	}
}