namespace SevenDigital.FeedMunch.Feeds.Schema
{
	public class Release
	{
		public string releaseId { get; set; }
		public string title { get; set; }
		public string version { get; set; }
		public string artistId { get; set; }
		public string artistAppearsAs { get; set; }
		public string barcode { get; set; }
		public string type { get; set; }
		public string year { get; set; }
		public string explicitContent { get; set; }
		public string trackCount { get; set; }
		public string duration { get; set; }
		public string tags { get; set; }
		public string licensorId { get; set; }
		public string image { get; set; }
		public string dateAdded { get; set; }
		public string releaseDate { get; set; }
		public string labelId { get; set; }
		public string labelName { get; set; }
		public string formats { get; set; }
		public string price { get; set; }
		public string rrp { get; set; }
		public string url { get; set; }
	}

	public class ReleaseIncremental : Release
	{
		public string action { get; set; }
	}
}