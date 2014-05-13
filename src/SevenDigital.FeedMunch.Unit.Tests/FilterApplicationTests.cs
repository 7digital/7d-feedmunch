using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SevenDigital.FeedMunch.Unit.Tests
{
	[TestFixture]
	public class FilterApplicationTests
	{
		[Test]
		public void No_filter_should_pass_through_csv_as_is()
		{
			var filter = new Filter("");
			var output = ApplyFilter(filter);

			Assert.That(ReadOutputStream(output), Is.EqualTo(DummyArtistFeedCsv()));
		}

		[Test]
		public void Filter_out_everything_but_keane()
		{
			var filter = new Filter("name=Keane");
			const string expected = 
				"artistId,name,popularity,tags,image,url\r\n" +
				"1,Keane,0.63,\"rock,pop,alternative,2000s\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000001_<$size$>.jpg,http://www.7digital.com/artist/keane/\r\n";

			var output = ApplyFilter(filter);

			Assert.That(ReadOutputStream(output), Is.EqualTo(expected));
		}

		[Test]
		public void Filter_out_keane()
		{
			var filter = new Filter("name!=Keane");
			var expected = RemoveAt(1);

			var output = ApplyFilter(filter);

			Assert.That(ReadOutputStream(output), Is.EqualTo(expected));
		}

		private static string RemoveAt(int pos)
		{
			var rows = DummyArtistFeedCsv().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			rows.RemoveAt(pos);
			return String.Join(Environment.NewLine, rows) + "\r\n";
		}

		private static Stream ApplyFilter(Filter filter)
		{
			var output = new MemoryStream();

			var dummyCsvInputStream = FillOutputStream(DummyArtistFeedCsv());

			filter.ApplyToStream(dummyCsvInputStream, output);

			return output;
		}

		private static Stream FillOutputStream(string data)
		{
			var dummyCsvInputStream = new MemoryStream();
			var sw = new StreamWriter(dummyCsvInputStream);

			sw.Write(data);
			sw.Flush();
			dummyCsvInputStream.Position = 0;

			return dummyCsvInputStream;
		}

		private static string ReadOutputStream(Stream output)
		{
			var sr = new StreamReader(output);
			output.Position = 0;
			var readOutputStream = sr.ReadToEnd();
			Console.WriteLine(readOutputStream);

			return readOutputStream;
		}

		private static string DummyArtistFeedCsv()
		{
			var sb = new StringBuilder();
			sb.AppendLine("artistId,name,popularity,tags,image,url");
			sb.AppendLine("1,Keane,0.63,\"rock,pop,alternative,2000s\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000001_<$size$>.jpg,http://www.7digital.com/artist/keane/");
			sb.AppendLine("2,Blink 182,0.6,\"rock,pop,punk,1990s,2000s,hard-rock-metal\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000002_<$size$>.jpg,http://www.7digital.com/artist/blink-182/");
			sb.AppendLine("4,Amy Winehouse,0.67,\"pop,randb-soul,singer-songwriter,2000s\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000004_<$size$>.jpg,http://www.7digital.com/artist/amy-winehouse/");
			sb.AppendLine("5,Busted,0.52,\"pop,dance,pop-rock\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000005_<$size$>.jpg,http://www.7digital.com/artist/busted/");
			sb.AppendLine("6,Sia,0.54,\"pop,electronic,2000s\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000006_<$size$>.jpg,http://www.7digital.com/artist/sia/");
			sb.AppendLine("7,Sugababes,0.57,\"pop,randb-soul,pop-rock,2000s\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000007_<$size$>.jpg,http://www.7digital.com/artist/sugababes/");
			sb.AppendLine("8,The Loose Cannons,0.39,,http://cdn.7static.com/static/img/artistimages/00/000/000/0000000008_<$size$>.jpg,http://www.7digital.com/artist/the-loose-cannons/");
			sb.AppendLine("9,Kid Symphony,0.34,\"pop,alternative\",http://cdn.7static.com/static/img/artistimages/00/000/000/0000000009_<$size$>.jpg,http://www.7digital.com/artist/kid-symphony/");
			sb.AppendLine("10,Chikinki,0.41,alternative,http://cdn.7static.com/static/img/artistimages/00/000/000/0000000010_<$size$>.jpg,http://www.7digital.com/artist/chikinki/");
			sb.AppendLine("85584,Trentemøller,0.5,alternative,http://cdn.7static.com/static/img/artistimages/00/000/855/0000085584_<$size$>.jpg,http://www.7digital.com/artist/trentemøller/");

			return sb.ToString();
		}
	}
}