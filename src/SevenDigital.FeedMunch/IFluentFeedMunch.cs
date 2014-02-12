using System;
using System.IO;

namespace SevenDigital.FeedMunch
{
	public interface IFeedStreamWriter
	{
		void Write(FeedMunchConfig feedMunchConfig, Action<Stream> writeFeedStream);
	}

	public interface IFluentFeedMunch
	{
		IFluentFeedMunch WithConfig(FeedMunchConfig config);
		void InvokeAndWriteTo(Stream outputStream);
		void InvokeAndWriteTo(IFeedStreamWriter feedStreamWriter);
	}
}