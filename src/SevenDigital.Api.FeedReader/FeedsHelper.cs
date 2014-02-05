using System;
using SevenDigital.Api.FeedReader.Dates;

namespace SevenDigital.Api.FeedReader
{
	public static class FeedsDateCreation
	{
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;

		public static string GetCurrentFullFeedDate(DateTime seedDate)
		{
			//return FirstOfTheCurrentMonth(seedDate);

			return FirstMondayOfTheCurrentWeek(seedDate);
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

	public static class SystemTime
	{
		public static Func<DateTime> _now = () => DateTime.Now;

		public static void SetTo(DateTime instance)
		{
			_now = () => instance;
		}
	}
}