using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DeCsv.Unit.Tests
{
	[TestFixture]
	public class End_to_end_deserialization_test
	{
		[Test]
		public void Can_deserialize_from_stream()
		{
			var testCsv = Enumerable.ToArray(TestData.TestCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
			var tempFileName = Path.GetTempFileName();
			File.WriteAllLines(tempFileName, testCsv);

			using (var fs = new FileStream(tempFileName, FileMode.Open, FileAccess.Read))
			{
				var queryRows = CsvSerializer.DeserializeEnumerableFromStream<QueryRow>(fs).ToList();
				Assert.That(queryRows.Count, Is.EqualTo(testCsv.Length - 1));
				Assert.That(queryRows[0].Artist, Is.EqualTo("Elton John"));
				Assert.That(queryRows[0].Country, Is.EqualTo("US"));
				Assert.That(queryRows[0].Query, Is.EqualTo("Your Song"));
				Assert.That(queryRows[0].Title, Is.EqualTo("Your Song"));
			}

			File.Delete(tempFileName);
		}
	}
}