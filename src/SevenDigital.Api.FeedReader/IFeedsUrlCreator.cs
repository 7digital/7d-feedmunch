namespace SevenDigital.Api.FeedReader
{
	public interface IFeedsUrlCreator
	{
		string SignUrlForLatestFeed(Feed feed);
	}
}