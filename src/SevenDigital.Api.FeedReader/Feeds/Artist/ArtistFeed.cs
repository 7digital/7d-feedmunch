using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFeed : Feed
	{
		private readonly IFileHelper _fileHelper;

		public ArtistFeed(IFileHelper fileHelper)
		{
			_fileHelper = fileHelper;
		}

		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return Path.Combine(_fileHelper.GetOrCreateFeedsFolder(), feedsDate + "-artist-feed.gz");
		}
	}
}