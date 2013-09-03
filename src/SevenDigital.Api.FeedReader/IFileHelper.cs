namespace SevenDigital.Api.FeedReader
{
	public interface IFileHelper
	{
		string GetOrCreateDirectoryAtRoot(string directoryName);
		string GetOrCreateFeedsFolder();
	}
}