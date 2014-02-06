namespace SevenDigital.Api.FeedReader
{
	public interface IFileHelper
	{
		string GetOrCreateOutputFolder(string directoryPath);
		string GenerateOutputFeedLocation(string output);
	}
}