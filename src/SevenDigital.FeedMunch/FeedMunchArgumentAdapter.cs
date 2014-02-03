using Args;

namespace SevenDigital.FeedMunch
{
	public class FeedMunchArgumentAdapter
	{
		public FeedMunchConfig FromConsoleArgs(string[] args)
		{
			return Configuration.Configure<FeedMunchConfig>().CreateAndBind(args);
		}
	}
}