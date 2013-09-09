using System.IO;

namespace SevenDigital.Api.FeedReader
{
	public class FeedsFileHelper : IFileHelper
	{
		private readonly string _feedsFolder;

		public FeedsFileHelper(string feedsFolder)
		{
			_feedsFolder = feedsFolder;
		}

		public string GetOrCreateDirectoryAtRoot(string directoryName)
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), directoryName);
			TryCreateDirectory(directory);
			return directory;
		}

		public string GetOrCreateFeedsFolder()
		{
			return GetOrCreateDirectoryAtRoot(_feedsFolder);
		}

		public bool FeedExists(Feed suppliedFeed)
		{
			var filePath = BuildFullFilepath(suppliedFeed);
			return File.Exists(filePath);
		}

		public string BuildFullFilepath(Feed suppliedFeed)
		{
			return Path.Combine(GetOrCreateFeedsFolder(), suppliedFeed.GetLatest());
		}

		private static void TryCreateDirectory(string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
		}
	}
}