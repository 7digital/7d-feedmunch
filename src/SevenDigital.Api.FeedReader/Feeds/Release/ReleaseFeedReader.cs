using System.Collections.Generic;
using DeCsv;

namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseFeedReader : IFeedReader<Schema.Release>
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public ReleaseFeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<Schema.Release> ReadIntoList(Feed feed)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feed);
			return CsvDeserialize.DeSerialize<Schema.Release>(decompressedStream);
		}
	}
}