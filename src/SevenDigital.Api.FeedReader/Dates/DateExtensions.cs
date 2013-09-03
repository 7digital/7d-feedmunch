using System;

namespace SevenDigital.Api.FeedReader.Dates
{
	public static class DateExtensions
	{
		public static DateTime NextDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
		{
			var start = (int)from.DayOfWeek;
			var target = (int)dayOfWeek;
			if (target <= start)
				target += 7;
			return from.AddDays(target - start);
		}

		public static DateTime PreviousDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
		{
			var start = (int)from.DayOfWeek;
			var target = (int)dayOfWeek;
			if (target >= start)
				target -= 7;
			return from.AddDays(target - start);
		}
	}
}