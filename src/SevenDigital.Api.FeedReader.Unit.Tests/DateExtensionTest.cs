using System;
using NUnit.Framework;
using SevenDigital.Api.FeedReader.Dates;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class DateExtensionTest
	{
		[Test]
		public void Next_returns_expected_date()
		{
			var dateTime = new DateTime(2012, 12, 29);
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Monday), Is.EqualTo(new DateTime(2012, 12, 31)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Tuesday), Is.EqualTo(new DateTime(2013, 1, 1)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Wednesday), Is.EqualTo(new DateTime(2013, 1, 2)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Thursday), Is.EqualTo(new DateTime(2013, 1, 3)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Friday), Is.EqualTo(new DateTime(2013, 1, 4)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Saturday), Is.EqualTo(new DateTime(2013, 1, 5)));
			Assert.That(dateTime.NextDayOfWeek(DayOfWeek.Sunday), Is.EqualTo(new DateTime(2012, 12, 30)));
		}

		[Test]
		public void Previous_returns_expected_dates()
		{
			var dateTime = new DateTime(2013, 01, 01);

			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Monday), Is.EqualTo(new DateTime(2012, 12, 31)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Tuesday), Is.EqualTo(new DateTime(2012, 12, 25)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Wednesday), Is.EqualTo(new DateTime(2012, 12, 26)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Thursday), Is.EqualTo(new DateTime(2012, 12, 27)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Friday), Is.EqualTo(new DateTime(2012, 12, 28)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Saturday), Is.EqualTo(new DateTime(2012, 12, 29)));
			Assert.That(dateTime.PreviousDayOfWeek(DayOfWeek.Sunday), Is.EqualTo(new DateTime(2012, 12, 30)));
		}
	}
}