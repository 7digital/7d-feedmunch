using System;
using System.IO;

namespace SevenDigital.Api.FeedReader
{
	public class FeedsFileHelper : IFileHelper
	{
		private readonly string _feedsFolder;
		private readonly string _outputFolder;

		public FeedsFileHelper(string feedsFolder, string outputFolder)
		{
			_feedsFolder = feedsFolder;
			_outputFolder = outputFolder;
		}

		public string GetOrCreateDirectoryAtRoot(string directoryName)
		{
			var path = Environment.CurrentDirectory;

			var directory = Path.Combine(path, directoryName);
			TryCreateDirectory(directory);
			return directory;
		}

		public string GetOrCreateFeedsFolder()
		{
			return GetOrCreateDirectoryAtRoot(_feedsFolder);
		}
		
		public string GetOrCreateOutputFolder(string path)
		{
			return GetOrCreateDirectoryAtRoot(Path.Combine(_outputFolder, path));
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
			{
				Directory.CreateDirectory(directory);
			}
		}
	}
}