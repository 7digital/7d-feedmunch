using System.IO;
using System.IO.Compression;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFeedUnpacker : IFeedUnpacker
	{
		private readonly Feed _artistFeed;

		public ArtistFeedUnpacker(Feed artistFeed)
		{
			_artistFeed = artistFeed;
		}

		public Stream GetDecompressedStream()
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(_artistFeed.GetLatest(), FileMode.Open, FileAccess.Read);
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