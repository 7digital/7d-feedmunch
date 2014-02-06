using System;
using System.IO;

namespace SevenDigital.Api.FeedReader
{
	public class FeedsFileHelper : IFileHelper
	{
		private readonly string _outputFolder;

		public FeedsFileHelper(string outputFolder)
		{
			_outputFolder = outputFolder;
		}

		public string GenerateOutputFeedLocation(string output)
		{
			var filename = Path.GetFileNameWithoutExtension(output);
			var directoryPath = Path.GetDirectoryName(output);
			var outputDirectory = GetOrCreateOutputFolder(directoryPath);
			return Path.Combine(outputDirectory, filename + ".tmp");
		}

		public string GetOrCreateOutputFolder(string path)
		{
			return GetOrCreateDirectoryAtRoot(Path.Combine(_outputFolder, path));
		}

		private static string GetOrCreateDirectoryAtRoot(string directoryName)
		{
			var path = Environment.CurrentDirectory;

			var directory = Path.Combine(path, directoryName);
			TryCreateDirectory(directory);
			return directory;
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