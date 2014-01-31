namespace SevenDigital.Api.FeedReader.Feeds.Schema
{
	public class Artist
	{
		public int ArtistId { get; set; }
		public string Name { get; set; }
		public string Popularity { get; set; }
		public string Tags { get; set; }
		public string Image { get; set; }
		public string Url { get; set; }
	}

	public class ArtistIncremental : Artist
	{
		public string Action { get; set; }
	}
}