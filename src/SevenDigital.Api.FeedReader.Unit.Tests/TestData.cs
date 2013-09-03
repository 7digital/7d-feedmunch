using System.Collections.Generic;
using System.IO;
using ServiceStack.Text;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	public static class TestData
	{
		public static string GetArtistCsv()
		{
			return CsvSerializer.SerializeToCsv(GetArtists());
		}

		public static Stream GetArtistCsvStream()
		{
			var memoryStream = new MemoryStream();

			var streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(GetArtistCsv());
			streamWriter.Flush();
			memoryStream.Position = 0;

			return memoryStream;
		}

		public static List<Artist> GetArtists()
		{
			var keane = new Artist
			{
				ArtistId = 1,
				Name = "Keane",
				Popularity = "0.64",
				Tags = "2000s",
				Image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000001_<$size$>.jpg",
				Url = "http://www.7digital.com/artist//artists/keane/"
			};
			var blink = new Artist
			{
				ArtistId = 2,
				Name = "Blink 182",
				Popularity = "0.6",
				Tags = "1990s,2000s,hard-rock-metal",
				Image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000002_<$size$>.jpg",
				Url = "http://www.7digital.com/artist//artists/blink182/"
			};
			var amy = new Artist
			{
				ArtistId = 3,
				Name = "Amy Winehouse",
				Popularity = "0.69",
				Tags = "singer-songwriter,2000s",
				Image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000003_<$size$>.jpg",
				Url = "http://www.7digital.com/artist//artists/amy-winehouse/"
			};

			return new List<Artist>
			{
				keane,
				blink,
				amy
			};
		}
	}
}