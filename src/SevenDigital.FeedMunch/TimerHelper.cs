using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

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

	public static class ConsoleFilePolling
	{
		public static Timer GenerateFileSizePollingTimer(string pathOfFileToPoll, int pollInterval)
		{
			return new Timer(x =>
			{
				if (!File.Exists(pathOfFileToPoll))
				{
					return;
				}

				var fileInfo = new FileInfo(pathOfFileToPoll);
				var length = fileInfo.Length;
				Console.CursorLeft = 0;
				Console.WriteLine(length);
				Console.CursorTop --;
				Console.CursorVisible = false;

			}, null, 0, pollInterval);
		}
	}
}