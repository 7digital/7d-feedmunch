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
				artistAppearsAs = "Salsa Passion",
				artistId = 323122,
				discNumber = 1,
				duration = 293,
				explicitContent = false,
				formats = "6,55,56,26",
				isrc = "GTA010701481",
				popularity = 0.38,
				price = 0.99M,
				rrp = 0.99M,
				streamingReleaseDate = new DateTime(2008, 03, 01),
				title = "Acuyuye",
				trackId = 4834997,
				trackNumber = 1,
				type = "Audio",
				url = "http://www.7digital.com/artist/salsa-passion/release/salsa-salsa?h=01",
				version = "",
				releaseId = 123,
				licensorID = 1
			};

			var smoothSailing = new Track
			{
				artistAppearsAs = "Queensof the Stone Age",
				artistId = 5845,
				discNumber = 1,
				duration = 291,
				explicitContent = false,
				formats = "17,33",
				isrc = "USMTD1303758",
				popularity = 0,
				price = 0.99M,
				releaseId = 2750601,
				rrp = 0.99M,
				streamingReleaseDate = new DateTime(2013, 06, 03),
				title = "Smooth Sailing",
				trackId = 29505540,
				trackNumber = 8,
				type = "Audio",
				url = "http://www.7digital.com/artist/queens-of-the-stone-age/release/like-clockwork/?partner=1401&amp;h=08",
				version = "",
				licensorID = 1
			};

			return new List<Track>
			{
				salsaPassion,
				smoothSailing
			};
		}
	}
}