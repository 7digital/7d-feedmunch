using Args;

namespace FeedMuncher
{
	public static class FeedMunchArgumentAdapter
	{
		public static ConsoleFeedMunchConfig FromConsoleArgs(string[] args)
		{
			return Configuration.Configure<ConsoleFeedMunchConfig>().CreateAndBind(args);
		}
	}
}