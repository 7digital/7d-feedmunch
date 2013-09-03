namespace SevenDigital.Api.FeedReader
{
	public interface IFeedsUrlCreator
	{
		string SignUrlForLatestArtistFeed(FeedType type, string countryCode);
	}
}