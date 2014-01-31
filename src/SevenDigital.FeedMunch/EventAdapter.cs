using System;

namespace SevenDigital.FeedMunch
{
	public class EventAdapter : IEventAdapter
	{
		public void Info(string message)
		{
			Console.WriteLine(message);
		}
	}
}