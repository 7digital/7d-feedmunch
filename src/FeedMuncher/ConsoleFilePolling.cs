using System;
using System.IO;
using System.Threading;

namespace FeedMuncher
{
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