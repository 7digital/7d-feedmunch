namespace SevenDigital.Api.FeedReader
{
	public interface IFeedsUrlCreator
	{
		string SignUrlForLatestFeed(FeedCatalogueType feedCatalogueType, FeedType type, string countryCode);
	}
}