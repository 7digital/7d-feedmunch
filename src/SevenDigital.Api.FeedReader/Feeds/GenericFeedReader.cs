using System.Collections.Generic;
using DeCsv;

namespace SevenDigital.Api.FeedReader.Feeds
{
	public class GenericFeedReader : IFeedReader
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public GenericFeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<T> ReadIntoList<T>(Feed feed)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feed);
			return CsvDeserialize.DeSerialize<T>(decompressedStream);
		}
	}
}