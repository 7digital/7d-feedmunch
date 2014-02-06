namespace SevenDigital.FeedMunch
{
	public interface IFluentFeedMunch
	{
		IFluentFeedMunch WithConfig(FeedMunchConfig config);
		void InvokeAndWriteToGzippedFile();
	}
}