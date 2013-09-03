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

		public IEnumerable<Schema.Artist> ReadIntoList()
		{
			var decompressedStream = _feedUnpacker.GetDecompressedStream();
			return CsvDeserialize.DeSerialize<Schema.Artist>(decompressedStream);
		}
	}
}