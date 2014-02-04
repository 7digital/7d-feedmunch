using System.Collections.Generic;
using DeCsv;

namespace SevenDigital.Api.FeedReader.Feeds.Track
{
	public class TrackFeedReader : IFeedReader<Schema.Track>
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public TrackFeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<Schema.Track> ReadIntoList(Feed feed)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feed);
			return CsvDeserialize.DeSerialize<Schema.Track>(decompressedStream);
		}
	}

	public class FeedReader : IFeedReader<object>
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public FeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<object> ReadIntoList(Feed feed)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feed);
			return CsvDeserialize.DeSerialize<object>(decompressedStream);
		}
	}
}