using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public interface IFeedDownload
	{
		bool FeedAlreadyExists(Feed suppliedFeed);
		string CurrentSignedUrl { get; }
		string CurrentFileName { get; }
		Task<Stream> DownloadToStream(Feed suppliedFeed);
	}

	public class FeedDownload : IFeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IFileHelper _fileHelper;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator, IFileHelper fileHelper)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_fileHelper = fileHelper;
		}

		public async Task<Stream> DownloadToStream(Feed suppliedFeed)
		{
			CurrentSignedUrl = _feedsUrlCreator.SignUrlForLatestFeed(suppliedFeed.CatalogueType, suppliedFeed.FeedType, suppliedFeed.Country);
			CurrentFileName = _fileHelper.BuildFullFilepath(suppliedFeed);

			var httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
			};
			return await httpClient.GetStreamAsync(CurrentSignedUrl);
		}

		public bool FeedAlreadyExists(Feed suppliedFeed)
		{
			return _fileHelper.FeedExists(suppliedFeed);
		}

		public string CurrentSignedUrl { get; private set; }
		public string CurrentFileName { get; private set; }
	}
}