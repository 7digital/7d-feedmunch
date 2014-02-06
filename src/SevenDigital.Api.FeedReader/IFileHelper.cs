namespace SevenDigital.Api.FeedReader
{
	public interface IFileHelper
	{
		string GetOrCreateFeedsFolder();
		bool FeedExists(Feed suppliedFeed);
		string BuildFullFilepath(Feed suppliedFeed);
		string GetOrCreateOutputFolder(string directoryPath);
		string GenerateOutputFeedLocation(string output);
	}
}