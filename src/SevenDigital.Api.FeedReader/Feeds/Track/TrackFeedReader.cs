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

		public IEnumerable<Schema.Track> ReadIntoList()
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(new TrackFullFeed());
			return CsvDeserialize.DeSerialize<Schema.Track>(decompressedStream);
		}
	}
}