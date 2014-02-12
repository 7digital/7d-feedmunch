using System;
using System.IO;
using System.IO.Compression;
using SevenDigital.FeedMunch;

namespace FeedMuncher
{
	public class GzippedFileFeedStreamWriter : IFeedStreamWriter
	{
		public void Write(FeedMunchConfig feedMunchConfig, Action<Stream> writeFeedStream)
		{
			var path = ((ConsoleFeedMunchConfig)feedMunchConfig).Output + ".tmp";
			using (var output = File.Create(path))
			{
				using (var gzipOut = new GZipStream(output, CompressionMode.Compress))
				{
					writeFeedStream(gzipOut);
				}
			}
			TryChangeExtension(path, ".tmp", ".gz"); 
		}

		private static void TryChangeExtension(string path, string from, string to)
		{
			var completedFilePath = path.Replace(from, to);
			if (File.Exists(completedFilePath))
			{
				File.Delete(completedFilePath);
			}
			File.Move(path, completedFilePath);
		}
	}
}