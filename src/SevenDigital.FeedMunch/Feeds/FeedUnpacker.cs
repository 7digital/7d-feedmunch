using System.IO;
using System.IO.Compression;

namespace SevenDigital.FeedMunch.Feeds
{
	public class FeedUnpacker : IFeedUnpacker
	{
		public FileStream GetFeedAsFilestream(Feed feed)
		{
			return new FileStream(feed.ExistingPath, FileMode.Open, FileAccess.Read);
		}

		public GZipStream GetDecompressedStream(Stream stream, Feed feed)
		{
			try
			{
				return new GZipStream(stream, CompressionMode.Decompress);
			}
			catch
			{
				stream.Close();
				throw;
			}
		}
	}
}