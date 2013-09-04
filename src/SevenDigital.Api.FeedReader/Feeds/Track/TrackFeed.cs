using System.IO;

namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackFeed : Feed
	{
		private readonly IFileHelper _fileHelper;

		public TrackFeed(IFileHelper fileHelper)
		{
			_fileHelper = fileHelper;
		}

		public override string GetLatest()
		{
			var feedsDate = GetPreviousFullFeedDate();
			return Path.Combine(_fileHelper.GetOrCreateFeedsFolder(), feedsDate + "-track-feed.gz");
		}
	}
}