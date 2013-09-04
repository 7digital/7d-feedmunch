using System.IO;
using System.IO.Compression;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class FeedUnpacker : IFeedUnpacker
	{
		private readonly Feed _feed;

		public FeedUnpacker(Feed feed)
		{
			_feed = feed;
		}

		public Stream GetDecompressedStream()
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(_feed.GetLatest(), FileMode.Open, FileAccess.Read);
				return new GZipStream(fileStream, CompressionMode.Decompress);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				throw;
			}
		}
	}
}