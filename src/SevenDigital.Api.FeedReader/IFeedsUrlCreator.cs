namespace SevenDigital.Api.FeedReader
{
	public interface IFeedsUrlCreator
	{
		string SignUrlForFeed(Feed feed);
	}
}