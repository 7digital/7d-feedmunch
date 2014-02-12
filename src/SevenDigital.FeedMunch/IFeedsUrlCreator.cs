namespace SevenDigital.FeedMunch
{
	public interface IFeedsUrlCreator
	{
		string SignUrlForFeed(Feed feed);
	}
}