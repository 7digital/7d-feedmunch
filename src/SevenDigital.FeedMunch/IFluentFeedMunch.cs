using System.IO;

namespace SevenDigital.FeedMunch
{
	public interface IFluentFeedMunch
	{
		IFluentFeedMunch WithConfig(FeedMunchConfig config);
		void InvokeAndWriteToGzippedFile();
		void InvokeAndWriteTo(Stream outputStream);
	}
}