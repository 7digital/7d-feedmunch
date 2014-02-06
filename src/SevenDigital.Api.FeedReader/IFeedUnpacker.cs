using System.IO;

namespace SevenDigital.Api.FeedReader
{
	public interface IFeedUnpacker
	{
		Stream GetDecompressedStream(Feed feed);
		Stream GetDecompressedStream(Stream stream, Feed feed);
	}
}