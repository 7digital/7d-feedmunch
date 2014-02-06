using System;
using System.Diagnostics;

namespace SevenDigital.FeedMunch
{
	public static class TimerHelper
	{
		public static Stopwatch TimeMe(Action toTime)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			toTime();
			stopwatch.Stop();
			return stopwatch;
		}
	}
}