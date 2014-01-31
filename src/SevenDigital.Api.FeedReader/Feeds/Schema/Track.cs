using System;

namespace SevenDigital.Api.FeedReader.Feeds.Schema
{
	public class Track
	{
		public int trackId { get; set; }
		public string title { get; set; }
		public string version { get; set; }
		public string type { get; set; }
		public string isrc { get; set; }
		public bool explicitContent { get; set; }
		public int trackNumber { get; set; }
		public int discNumber { get; set; }
		public int artistId { get; set; }
		public string artistAppearsAs { get; set; }
		public int releaseId { get; set; }
		public int duration { get; set; }
		public string formats { get; set; }
		public decimal price { get; set; }
		public decimal rrp { get; set; }
		public string url { get; set; }
		public double  popularity { get; set; }
		public DateTime streamingReleaseDate { get; set; }
		public int licensorID { get; set; }
	}

	public class TrackIncremental : Track
	{
		public string action { get; set; }
	}
}