using System.Collections.Generic;

namespace SevenDigital.Api.FeedReader
{
	public interface IFeedReader<T>
	{
		IEnumerable<T> ReadIntoList();
	}
}