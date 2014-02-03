using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Unit.Tests
{
	[TestFixture]
	public class FilteringTest
	{
		[Test]
		public void Tets()
		{
			var terence = new Artist
			{
				name = "terence"
			};

			var bill = new Artist
			{
				name = "bill"
			};

			var artists = new List<object>
			{
				terence,
				bill
			};

			var filter = new Filter("name=bill");

			var filteredArtists = artists.Where(x =>
			{
				var type = x.GetType();
				var propertyInfo = type.GetProperty(filter.FieldName);
				var methodInfo = propertyInfo.GetGetMethod();
				var invoke = methodInfo.Invoke(x, null);
				return (string)invoke == filter.Values.First();
			}).Cast<Artist>().ToList();

			Assert.That(filteredArtists.Count, Is.EqualTo(1));
			Assert.That(filteredArtists[0].name, Is.EqualTo("bill"));
		}
	}

	[TestFixture]
	public class FilterParseTests
	{
		[Test]
		public void Finds_two_elements_and_comparitor_when_equals_used()
		{
			const string filter = "licensorId=1";

			var filterD = new Filter(filter);

			Assert.That(filterD.FieldName, Is.EqualTo("licensorId"));
			Assert.That(filterD.Values.First(), Is.EqualTo("1"));
			Assert.That(filterD.Operator, Is.EqualTo(FilterOperator.Equals));
		}

		[Test]
		public void Finds_two_elements_and_comparitor_when_not_equals_used()
		{
			const string filter = "licensorId!=1";

			var filterD = new Filter(filter);

			Assert.That(filterD.FieldName, Is.EqualTo("licensorId"));
			Assert.That(filterD.Values.First(), Is.EqualTo("1"));
			Assert.That(filterD.Operator, Is.EqualTo(FilterOperator.NotEquals));
		}

		[Test]
		public void Throws_meaningful_exception_if_cannot_parse()
		{
			var argumentException = Assert.Throws<ArgumentException>(() => new Filter("yo mamma"));
			Assert.That(argumentException.Message, Is.EqualTo("Could not parse filter, should be in the format {fieldName}[=]|[!=]{array of values} e.g. licensorId=1,2,3 "));
		}

		[Test]
		public void Can_produce_filter_With_multiple_Values()
		{
			const string filter = "licensorId!=1,2,3";

			var filterD = new Filter(filter);

			Assert.That(filterD.FieldName, Is.EqualTo("licensorId"));
			var filterValues = filterD.Values.ToArray();
			Assert.That(filterValues.Length, Is.EqualTo(3));
			Assert.That(filterValues[0], Is.EqualTo("1"));
			Assert.That(filterValues[1], Is.EqualTo("2"));
			Assert.That(filterValues[2], Is.EqualTo("3"));
			Assert.That(filterD.Operator, Is.EqualTo(FilterOperator.NotEquals));
		}
	}
}