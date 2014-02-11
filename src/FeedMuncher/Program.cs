using System.IO;
using System.IO.Compression;
using FeedMuncher.IOC.StructureMap;

namespace FeedMuncher
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrap.ConfigureDependencies();

			var feedMunchConfig = FeedMunch.Configure.FromConsoleArgs(args);

			using (var output = File.Create(feedMunchConfig.Output))
			{
				using (var gzipOut = new GZipStream(output, CompressionMode.Compress))
				{
					FeedMunch.Download
						.WithConfig(feedMunchConfig)
						.InvokeAndWriteTo(gzipOut);
				}
			}
			TryChangeExtension(feedMunchConfig.Output, ".tmp", ".gz");
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
