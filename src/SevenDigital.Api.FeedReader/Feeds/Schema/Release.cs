namespace SevenDigital.Api.FeedReader.Feeds.Schema
{
	public class Release
	{}

	public class ReleaseIncremental : Release
	{
		public string Action { get; set; }
	}
}