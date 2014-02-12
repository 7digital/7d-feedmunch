namespace SevenDigital.FeedMunch.Feeds.Schema
{
	public class Artist
	{
		public int artistId { get; set; }
		public string name { get; set; }
		public string popularity { get; set; }
		public string tags { get; set; }
		public string image { get; set; }
		public string url { get; set; }
	}

	public class ArtistIncremental : Artist
	{
		public string action { get; set; }
	}
}