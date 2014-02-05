namespace SevenDigital.FeedMunch
{
	public interface ILogAdapter
	{
		void Info(string message);
		void Error(string message);
	}
}
