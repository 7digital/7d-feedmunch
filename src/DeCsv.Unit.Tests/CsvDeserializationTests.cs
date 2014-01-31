using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DeCsv.Unit.Tests
{
	[TestFixture]
	public class CsvDeserializationTests
	{
		[Test]
		public void Should_deserialize_correct_csv()
		{
			var queryRows = CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsv).ToList();
			Assert.That(queryRows.Count, Is.EqualTo(2));
			Assert.That(queryRows[0].Artist, Is.EqualTo("Elton John"));
			Assert.That(queryRows[0].Country, Is.EqualTo("US"));
			Assert.That(queryRows[0].Query, Is.EqualTo("Your Song"));
			Assert.That(queryRows[0].Title, Is.EqualTo("Your Song"));
			Assert.That(queryRows[0].Date, Is.EqualTo(new DateTime(2008, 03, 01)));
		}

		[Test]
		public void Should_deserialize_csv_that_contains_blank_column()
		{
			var queryRows = CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvBlankColumn).ToList();
			Assert.That(queryRows.Count, Is.EqualTo(1));
			Assert.That(queryRows[0].Artist, Is.EqualTo("Guns 'n Roses"));
			Assert.That(queryRows[0].Country, Is.EqualTo("US"));
			Assert.That(queryRows[0].Query, Is.EqualTo(""));
			Assert.That(queryRows[0].Title, Is.EqualTo("Patience"));
			Assert.That(queryRows[0].Date, Is.EqualTo(new DateTime(2008, 03, 01)));

		}

		[Test]
		public void Should_deserialize_csv_that_contains_comma()
		{
			var queryRows = CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvCommaColumn).ToList();
			Assert.That(queryRows.Count, Is.EqualTo(1));
			Assert.That(queryRows[0].Artist, Is.EqualTo("Oasis"));
			Assert.That(queryRows[0].Country, Is.EqualTo("UK"));
			Assert.That(queryRows[0].Query, Is.EqualTo("Definately, Maybe"));
			Assert.That(queryRows[0].Title, Is.EqualTo("Definately, Maybe"));
			Assert.That(queryRows[0].Date, Is.EqualTo(new DateTime(2008, 03, 01)));

		}

		[Test]
		public void Should_deserialize_csv_thats_missing_final_comma()
		{
			var queryRows = CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvMissingFinalComma).ToList();
			Assert.That(queryRows.Count, Is.EqualTo(2));
			Assert.That(queryRows[0].Artist, Is.EqualTo("Elton John"));
			Assert.That(queryRows[0].Country, Is.EqualTo("US"));
			Assert.That(queryRows[0].Query, Is.EqualTo("Your Song"));
			Assert.That(queryRows[0].Title, Is.EqualTo("Your Song"));
			Assert.That(queryRows[0].Date, Is.EqualTo(new DateTime(2008, 03, 01)));
			Assert.That(queryRows[1].Price, Is.EqualTo(0));
		}

		[Test]
		public void Should_deserialize_csv_that_contains_blank_price()
		{
			var queryRows = CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvBlankPrice).ToList();
			Assert.That(queryRows.Count, Is.EqualTo(1));
			Assert.That(queryRows[0].Price, Is.EqualTo(0));

		}

		[Test]
		public void Should_throw_meaningful_execption_if_header_row_doesnt_match_type()
		{
			var testCsv = TestData.TestCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
			var csvDeserializationException = Assert.Throws<CsvDeserializationException>(() => CsvDeserialize.DeSerialize<QueryRow>(testCsv));

			Assert.That(csvDeserializationException.Message, Is.StringStarting("PropertyName \"US\" is not a property of type "));
		}

		[Test]
		public void Should_throw_meaningful_exception_if_row_columns_length_greater_than_header_columns()
		{
			var csvDeserializationException = Assert.Throws<CsvDeserializationException>(() => CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvNotMatching).ToList());

			Assert.That(csvDeserializationException.Message, Is.EqualTo("Row length is greater than header row length"));

			Assert.That(csvDeserializationException.RowRaw, Is.EqualTo("UK,Definately, Maybe,Oasis,Definately, Maybe,false,2008-03-01T00:00:00Z,0.99"));
			Assert.That(csvDeserializationException.RowFields.Count(), Is.EqualTo(9));
			Assert.That(csvDeserializationException.HeaderFields.Count(), Is.EqualTo(7));
			string actual = csvDeserializationException.ToString();
			Assert.That(actual, Is.EqualTo("Row length is greater than header row length Rows: UK,Definately, Maybe,Oasis,Definately, Maybe,false,2008-03-01T00:00:00Z,0.99 Headers:Country,Query,Artist,Title,Ignore,Date,Price"));
		}

		[Test]
		public void Should_not_throw_exception_if_row_columns_length_less_than_header_columns()
		{
			Assert.DoesNotThrow(() => CsvDeserialize.DeSerialize<QueryRow>(TestData.TestCsvRowLessThanHeader).ToList());
		}

		[Test]
		public void Can_deserialize_from_stream()
		{
			var testCsv = TestData.TestCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();
			var tempFileName = Path.GetTempFileName();
			File.WriteAllLines(tempFileName, testCsv);

			using (var fs = new FileStream(tempFileName, FileMode.Open, FileAccess.Read))
			{
				var queryRows = CsvDeserialize.DeSerialize<QueryRow>(fs).ToList();
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