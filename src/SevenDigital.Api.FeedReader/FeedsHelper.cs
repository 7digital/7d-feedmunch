using System;
using SevenDigital.Api.FeedReader.Dates;

namespace SevenDigital.Api.FeedReader
{
	public static class FeedsDateCreation
	{
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;

		public static string GetCurrentFeedDate(DateTime seedDate, FeedType feedType)
		{
			//return FirstOfTheCurrentMonth(seedDate);

			return feedType == FeedType.Full ? FirstMondayOfTheCurrentWeek(seedDate) : seedDate.ToString("yyyyMMdd");
		}

		private static string FirstOfTheCurrentMonth(DateTime seedDate)
		{
			return new DateTime(seedDate.Year, seedDate.Month, 1).ToString("yyyyMMdd");
		}

		private static string FirstMondayOfTheCurrentWeek(DateTime seedDate)
		{
			return seedDate.PreviousDayOfWeek(FULL_FEED_DAY_OF_WEEK).ToString("yyyyMMdd");
		}
	}
}