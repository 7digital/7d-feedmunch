using System;
using System.IO;
using System.IO.Compression;
using FeedMuncher.IOC.StructureMap;
using SevenDigital.FeedMunch;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunchArgumentAdapter.FromConsoleArgs(args);

			WriteStreamToGzippedFile(feedMunchConfig, stream =>
				FeedMunch.Download
					.WithConfig(feedMunchConfig)
					.InvokeAndWriteTo(stream)
			);
		}

		private static void WriteStreamToGzippedFile(ConsoleFeedMunchConfig feedMunchConfig, Action<Stream> generateStream)
		{
			var path = feedMunchConfig.Output + ".tmp";
			using (var output = File.Create(path))
			{
				using (var gzipOut = new GZipStream(output, CompressionMode.Compress))
				{
					generateStream(gzipOut);
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
