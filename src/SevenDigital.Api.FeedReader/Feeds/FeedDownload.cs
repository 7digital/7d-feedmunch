﻿using System.IO;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class FeedDownload
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

		public void SaveLocally(Feed suppliedFeed)
		{
			if (FeedAlreadyExists(suppliedFeed)) return;

			var signedFeedsUrl = _feedsUrlCreator.SignUrlForLatestFeed(suppliedFeed.FeedCatalogueType(), FeedType.Full, "GB");

			var fileName = BuildFullFilepath(suppliedFeed);
			_webClient.DownloadFile(signedFeedsUrl, fileName);
		}

		public bool FeedAlreadyExists(Feed suppliedFeed)
		{
			var fileName = BuildFullFilepath(suppliedFeed);
			return File.Exists(fileName);
		}

		private string BuildFullFilepath(Feed suppliedFeed)
		{
			return Path.Combine(_fileHelper.GetOrCreateFeedsFolder(), suppliedFeed.GetLatest());
		}
	}
}