using System.Collections.Generic;
using DeCsv;

namespace SevenDigital.Api.FeedReader.Feeds.Release
{
	public class ReleaseFeedReader : IFeedReader<Release>
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public ReleaseFeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<Release> ReadIntoList()
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(new ReleaseFullFeed());
			return CsvDeserialize.DeSerialize<Release>(decompressedStream);
		}
	}
}