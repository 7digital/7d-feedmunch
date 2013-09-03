using System.IO;

namespace SevenDigital.Api.FeedReader
{
	public interface IFeedUnpacker
	{
		Stream GetDecompressedStream();
	}
}