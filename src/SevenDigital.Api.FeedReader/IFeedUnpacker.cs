using System.IO;
using System.IO.Compression;

namespace SevenDigital.Api.FeedReader
{
	public interface IFeedUnpacker
	{
		FileStream GetFeedAsFilestream(Feed feed);
		GZipStream GetDecompressedStream(Stream stream, Feed feed);
	}
}