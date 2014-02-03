using Args;

namespace SevenDigital.FeedMunch
{
	public class FeedMunchArgumentAdapter
	{
		public FeedMunchConfig ToConfig(string[] args)
		{
			return Configuration.Configure<FeedMunchConfig>().CreateAndBind(args);
		}
	}
}