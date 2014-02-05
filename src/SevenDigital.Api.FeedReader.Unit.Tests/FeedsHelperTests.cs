using System;
using NUnit.Framework;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class FeedsHelperTests
	{
		[Test]
		public void Current_full_feed_date_should_be_beginning_of_month()
		{
			var seedDate = new DateTime(2012, 1, 22);
			var currentFullFeedDate = FeedsDateCreation.GetCurrentFullFeedDate(seedDate);

			Assert.That(currentFullFeedDate, Is.EqualTo("20120101"));
		}

		[Test]
		public void Current_full_feed_date_should_be_beginning_of_month_if_first()
		{
			var seedDate = new DateTime(2012, 1, 1);
			var currentFullFeedDate = FeedsDateCreation.GetCurrentFullFeedDate(seedDate);

			Assert.That(currentFullFeedDate, Is.EqualTo("20120101"));
		}

		[Test]
		public void Current_full_feed_date_should_be_beginning_of_month_if_last()
		{
			var seedDate = new DateTime(2014, 02, 28);
			var currentFullFeedDate = FeedsDateCreation.GetCurrentFullFeedDate(seedDate);

			Assert.That(currentFullFeedDate, Is.EqualTo("20140201"));
		}
	}
}