using System;

namespace SevenDigital.FeedMunch
{
	public class ConsoleLogAdapter : ILogAdapter
	{
		public void Info(string message)
		{
			Console.WriteLine("INFO: {0} ", message);
		}

		public void Error(string message)
		{
			Console.WriteLine("ERROR: {0} ", message);
		}
	}
}