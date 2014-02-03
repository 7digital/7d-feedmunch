using System.Threading.Tasks;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public interface IFeedDownload
	{
		Task SaveLocally(Feed suppliedFeed);
		bool FeedAlreadyExists(Feed suppliedFeed);
		string CurrentSignedUrl { get; }
		string CurrentFileName { get; }
	}

	public class FeedDownload : IFeedDownload
	{
		private readonly IFeedsUrlCreator _feedsUrlCreator;
		private readonly IWebClientWrapper _webClient;
		private readonly IFileHelper _fileHelper;

		public FeedDownload(IFeedsUrlCreator feedsUrlCreator, IWebClientWrapper webClient, IFileHelper fileHelper)
		{
			_feedsUrlCreator = feedsUrlCreator;
			_webClient = webClient;
			_fileHelper = fileHelper;
		}

		public async Task SaveLocally(Feed suppliedFeed)
		{
			CurrentSignedUrl = _feedsUrlCreator.SignUrlForLatestFeed(suppliedFeed.GetCatalogueType(), suppliedFeed.GetFeedType(), suppliedFeed.CountryCode);
			CurrentFileName = _fileHelper.BuildFullFilepath(suppliedFeed);

			if (FeedAlreadyExists(suppliedFeed) && suppliedFeed.WriteMethod == FeedWriteMethod.ResumeIfExists)
			{
				await _webClient.ResumeDownloadFile(CurrentSignedUrl, CurrentFileName);
			} 
			else if (FeedAlreadyExists(suppliedFeed) && suppliedFeed.WriteMethod == FeedWriteMethod.ForceOverwriteIfExists)
			{}
			else
			{
				await _webClient.DownloadFile(CurrentSignedUrl, CurrentFileName);
			}
		}

		public bool FeedAlreadyExists(Feed suppliedFeed)
		{
			return _fileHelper.FeedExists(suppliedFeed);
		}

		public string CurrentSignedUrl { get; private set; }
		public string CurrentFileName { get; private set; }
	}
}