using System;

namespace SevenDigital.Api.FeedReader.Feeds.Schema
{
	public class Track
	{
		public int TrackId { get; set; }
		public string Title { get; set; }
		public string Version { get; set; }
		public string Type { get; set; }
		public string Isrc { get; set; }
		public bool ExplicitContent { get; set; }
		public int TrackNumber { get; set; }
		public int DiscNumber { get; set; }
		public int ArtistId { get; set; }
		public string ArtistAppearsAs { get; set; }
		public int ReleaseId { get; set; }
		public int Duration { get; set; }
		public string Formats { get; set; }
		public decimal Price { get; set; }
		public decimal Rrp { get; set; }
		public string Url { get; set; }
		public double  Popularity { get; set; }
		public DateTime StreamingReleaseDate { get; set; }
		public int LicensorID { get; set; }
	}

	public class TrackIncremental : Track
	{
		public string Action { get; set; }
	}
}