using System.IO;
using System.IO.Compression;

namespace SevenDigital.FeedMunch
{
	public interface IFeedUnpacker
	{
		FileStream GetFeedAsFilestream(Feed feed);
		GZipStream GetDecompressedStream(Stream stream, Feed feed);
	}
}