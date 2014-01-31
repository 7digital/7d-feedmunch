using System;
using System.Collections.Generic;
using System.IO;
using ServiceStack.Text;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.Api.FeedReader.Unit.Tests.TestData
{
	public static class TrackData
	{
		public static string GetCsv()
		{
			return CsvSerializer.SerializeToCsv(GetTracks());
		}

		public static Stream GetCsvStream()
		{
			var memoryStream = new MemoryStream();

			var streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(GetCsv());
			streamWriter.Flush();
			memoryStream.Position = 0;

			return memoryStream;
		}

		public static List<Track> GetTracks()
		{
			var salsaPassion = new Track
			{
				ArtistAppearsAs = "Salsa Passion",
				ArtistId = 323122,
				DiscNumber = 1,
				Duration = 293,
				ExplicitContent = false,
				Formats = "6,55,56,26",
				Isrc = "GTA010701481",
				Popularity = 0.38,
				Price = 0.99M,
				ReleaseId = 433972,
				Rrp = 0.99M,
				StreamingReleaseDate = new DateTime(2008, 03, 01),
				Title = "Acuyuye",
				TrackId = 4834997,
				TrackNumber = 1,
				Type = "Audio",
				Url = "http://www.7digital.com/artist/salsa-passion/release/salsa-salsa?h=01",
				Version = ""
			};

			var smoothSailing = new Track
			{
				ArtistAppearsAs = "Queensof the Stone Age",
				ArtistId = 5845,
				DiscNumber = 1,
				Duration = 291,
				ExplicitContent = false,
				Formats = "17,33",
				Isrc = "USMTD1303758",
				Popularity = 0,
				Price = 0.99M,
				ReleaseId = 2750601,
				Rrp = 0.99M,
				StreamingReleaseDate = new DateTime(2013, 06, 03),
				Title = "Smooth Sailing",
				TrackId = 29505540,
				TrackNumber = 8,
				Type = "Audio",
				Url = "http://www.7digital.com/artist/queens-of-the-stone-age/release/like-clockwork/?partner=1401&amp;h=08",
				Version = ""
			};

			return new List<Track>
			{
				salsaPassion,
				smoothSailing
			};
		}
	}
}