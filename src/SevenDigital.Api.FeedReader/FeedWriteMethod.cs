namespace SevenDigital.Api.FeedReader
{
	public enum FeedWriteMethod
	{
		ResumeIfExists = 0, // Resumes feed download as if feed is a partial
		ForceOverwriteIfExists = 1, // Overwrites existing feed if feed filename the same
		IgnoreIfExists = 2 // Ignores download existing feed found if filename the same
	}
}