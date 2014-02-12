using System.Collections.Generic;
using System.IO;
using ServiceStack.Text;
using SevenDigital.FeedMunch.Feeds.Schema;

namespace SevenDigital.FeedMunch.Unit.Tests.TestData
{
	public static class ArtistData
	{
		public static string GetCsv()
		{
			return CsvSerializer.SerializeToCsv(GetArtists());
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

		public static List<Artist> GetArtists()
		{
			var keane = new Artist
			{
				artistId = 1,
				name = "Keane",
				popularity = "0.64",
				tags = "2000s",
				image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000001_<$size$>.jpg",
				url = "http://www.7digital.com/artist//artists/keane/"
			};
			var blink = new Artist
			{
				artistId = 2,
				name = "Blink 182",
				popularity = "0.6",
				tags = "1990s,2000s,hard-rock-metal",
				image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000002_<$size$>.jpg",
				url = "http://www.7digital.com/artist//artists/blink182/"
			};
			var amy = new Artist
			{
				artistId = 3,
				name = "Amy Winehouse",
				popularity = "0.69",
				tags = "singer-songwriter,2000s",
				image = "http://cdn.7static.com/static/img/artistimages/00/000/000/0000000003_<$size$>.jpg",
				url = "http://www.7digital.com/artist//artists/amy-winehouse/"
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