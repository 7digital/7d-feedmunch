using System.Collections.Generic;

namespace SevenDigital.Api.FeedReader
{
	public interface IFeedReader<T>
	{
		IEnumerable<T> ReadIntoList(Feed feed);
	}

	public interface IFeedReader
	{
		IEnumerable<T> ReadIntoList<T>(Feed feed);
	}
}