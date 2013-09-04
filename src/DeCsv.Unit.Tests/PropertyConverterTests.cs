﻿using System;
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

			var testObject = new Test();
			var shortProperty = PropertyConvertor.GetProperty<Test>("Short");
			PropertyConvertor.SetValue(testObject, shortProperty, short.MaxValue.ToString());
			Assert.That(testObject.Short, Is.EqualTo(short.MaxValue));

			var intProperty = PropertyConvertor.GetProperty<Test>("Int");
			PropertyConvertor.SetValue(testObject, intProperty, int.MaxValue.ToString());
			Assert.That(testObject.Int, Is.EqualTo(int.MaxValue));

			var longProperty = PropertyConvertor.GetProperty<Test>("Long");
			PropertyConvertor.SetValue(testObject, longProperty, long.MaxValue.ToString());
			Assert.That(testObject.Long, Is.EqualTo(long.MaxValue));

			var decimalProperty = PropertyConvertor.GetProperty<Test>("Decimal");
			PropertyConvertor.SetValue(testObject, decimalProperty, decimal.MaxValue.ToString());
			Assert.That(testObject.Decimal, Is.EqualTo(decimal.MaxValue));
		}

		[Test]
		public void Can_deal_with_datetime_transforms()
		{
			var testObject = new Test();
			var property = PropertyConvertor.GetProperty<Test>("TImestamp");
			PropertyConvertor.SetValue(testObject, property, DateTime.MinValue.ToString());
			Assert.That(testObject.TImestamp, Is.EqualTo(DateTime.MinValue));
		}

		internal class Test
		{
			public short Short { get; set; }
			public int Int { get; set; }
			public long Long { get; set; }
			public decimal Decimal { get; set; }
			public DateTime TImestamp { get; set; }
		}

	}
}