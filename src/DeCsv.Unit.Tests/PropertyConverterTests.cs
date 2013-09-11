using System;
using NUnit.Framework;

namespace DeCsv.Unit.Tests
{
	[TestFixture]
	public class PropertyConverterTests
	{
		[Test]
		public void Can_deal_with_string_to_bool_transform()
		{
			var propertyInfo = PropertyConvertor.GetProperty<QueryRow>("Ignore");

			var queryRow = new QueryRow();
			PropertyConvertor.SetValue(queryRow, propertyInfo, "false");
			Assert.That(queryRow.Ignore, Is.False);

			PropertyConvertor.SetValue(queryRow, propertyInfo, "true");
			Assert.That(queryRow.Ignore);
		}

		[Test]
		public void Can_deal_with_numeric_transforms()
		{
			var fakeObjectToConvertTo = new Test();

			var shortProperty = PropertyConvertor.GetProperty<Test>("Short");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, shortProperty, short.MaxValue.ToString());
			Assert.That(fakeObjectToConvertTo.Short, Is.EqualTo(short.MaxValue));

			var intProperty = PropertyConvertor.GetProperty<Test>("Int");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, intProperty, int.MaxValue.ToString());
			Assert.That(fakeObjectToConvertTo.Int, Is.EqualTo(int.MaxValue));

			var longProperty = PropertyConvertor.GetProperty<Test>("Long");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, longProperty, long.MaxValue.ToString());
			Assert.That(fakeObjectToConvertTo.Long, Is.EqualTo(long.MaxValue));

			var decimalProperty = PropertyConvertor.GetProperty<Test>("Decimal");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, decimalProperty, decimal.MaxValue.ToString());
			Assert.That(fakeObjectToConvertTo.Decimal, Is.EqualTo(decimal.MaxValue));
		}

		[Test]
		public void Can_deal_with_blank_numeric_transforms()
		{
			var fakeObjectToConvertTo = new Test();
			var emptyString = string.Empty;
			const int expected = 0;

			var shortProperty = PropertyConvertor.GetProperty<Test>("Short");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, shortProperty, emptyString);
			Assert.That(fakeObjectToConvertTo.Short, Is.EqualTo(expected));

			var intProperty = PropertyConvertor.GetProperty<Test>("Int");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, intProperty, emptyString);
			Assert.That(fakeObjectToConvertTo.Int, Is.EqualTo(expected));

			var longProperty = PropertyConvertor.GetProperty<Test>("Long");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, longProperty, emptyString);
			Assert.That(fakeObjectToConvertTo.Long, Is.EqualTo(expected));

			var decimalProperty = PropertyConvertor.GetProperty<Test>("Decimal");
			PropertyConvertor.SetValue(fakeObjectToConvertTo, decimalProperty, emptyString);
			Assert.That(fakeObjectToConvertTo.Decimal, Is.EqualTo(expected));
		}

		[Test]
		public void Can_deal_with_datetime_transforms()
		{
			var testObject = new Test();
			var property = PropertyConvertor.GetProperty<Test>("Timestamp");
			PropertyConvertor.SetValue(testObject, property, DateTime.MinValue.ToString());
			Assert.That(testObject.Timestamp, Is.EqualTo(DateTime.MinValue));
		}

		[Test]
		public void Can_deal_with_blank_datetime_transforms()
		{
			var testObject = new Test();
			var property = PropertyConvertor.GetProperty<Test>("Timestamp");
			PropertyConvertor.SetValue(testObject, property, string.Empty);
			Assert.That(testObject.Timestamp, Is.EqualTo(DateTime.MinValue));
		}

		internal class Test
		{
			public short Short { get; set; }
			public int Int { get; set; }
			public long Long { get; set; }
			public decimal Decimal { get; set; }
			public DateTime Timestamp { get; set; }
		}

	}
}