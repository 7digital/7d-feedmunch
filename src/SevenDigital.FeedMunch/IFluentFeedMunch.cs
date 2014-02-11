using System.IO;

namespace SevenDigital.FeedMunch
{
	public interface IFluentFeedMunch
	{
		IFluentFeedMunch WithConfig(FeedMunchConfig config);
		void InvokeAndWriteTo(Stream outputStream);
	}
}