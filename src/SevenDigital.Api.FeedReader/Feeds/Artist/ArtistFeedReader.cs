using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

	public class FeedReader 
	{
		private readonly IFeedUnpacker _feedUnpacker;

		public FeedReader(IFeedUnpacker feedUnpacker)
		{
			_feedUnpacker = feedUnpacker;
		}

		public IEnumerable<Schema.Artist> Artist(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.Artist>(GetDecompressedStream(feed));
		}

		public IEnumerable<Schema.Release> Release(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.Release>(GetDecompressedStream(feed));
		}

		public IEnumerable<Schema.Track> Track(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.Track>(GetDecompressedStream(feed));
		}

		public IEnumerable<Schema.ArtistIncremental> ArtistIncremental(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.ArtistIncremental>(GetDecompressedStream(feed));
		}

		public IEnumerable<Schema.ReleaseIncremental> ReleaseIncremental(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.ReleaseIncremental>(GetDecompressedStream(feed));
		}

		public IEnumerable<Schema.TrackIncremental> TrackIncremental(Feed feed)
		{
			return CsvDeserialize.DeSerialize<Schema.TrackIncremental>(GetDecompressedStream(feed));
		}

		private Stream GetDecompressedStream(Feed feed)
		{
			return _feedUnpacker.GetDecompressedStream(feed);
		}
	}
}