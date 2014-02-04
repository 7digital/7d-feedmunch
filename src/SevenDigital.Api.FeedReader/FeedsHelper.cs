using System;

namespace SevenDigital.Api.FeedReader
{
	public static class FeedsHelper
	{
		public static string GetCurrentFullFeedDate(DateTime seedDate)
		{
			return FirstOfTheCurrentMonth(seedDate);
		}

		private static string FirstOfTheCurrentMonth(DateTime seedDate)
		{
			return new DateTime(seedDate.Year, seedDate.Month, 1).ToString("yyyyMMdd");
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