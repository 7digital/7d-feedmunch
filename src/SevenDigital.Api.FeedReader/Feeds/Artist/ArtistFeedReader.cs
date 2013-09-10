using System.Collections.Generic;
using DeCsv;

namespace SevenDigital.Api.FeedReader.Feeds.Artist
{
	public class ArtistFeedReader : IFeedReader<Schema.Artist>
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public ArtistFeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<Schema.Artist> ReadIntoList(Feed feed)
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream(feed);
			return CsvDeserialize.DeSerialize<Schema.Artist>(decompressedStream);
		}
	}
}