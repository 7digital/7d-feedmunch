using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DeCsv;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds;
using SevenDigital.Api.FeedReader.Feeds.Schema;
using SevenDigital.Api.FeedReader.Unit.Tests.TestData;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class GenericFeedReaderTests
	{
		[Test]
		public void Can_read_from_stub_artist_feed()
		{
			var artistCsvStream = ArtistData.GetCsvStream();
			var artistFeedFetcher = MockRepository.GenerateStub<IFeedUnpacker>();

			var artistFullFeed = AvailableFeeds.ArtistFull;
			artistFeedFetcher.Stub(x => x.GetDecompressedStream(artistFullFeed)).IgnoreArguments().Return(artistCsvStream);

			var artistFeedReader = new GenericFeedReader(artistFeedFetcher);

			var readFromFeeds = artistFeedReader.ReadIntoList<Artist>(artistFullFeed);

			Assert.That(readFromFeeds.Count(), Is.EqualTo(3));

			artistCsvStream.Dispose();
		}

		[Test]
		public void Can_read_from_stub_track_feed()
		{
			var csvStream = TrackData.GetCsvStream();
			var feedUnpacker = MockRepository.GenerateStub<IFeedUnpacker>();

			var trackFullFeed = AvailableFeeds.TrackFull;
			feedUnpacker.Stub(x => x.GetDecompressedStream(trackFullFeed)).IgnoreArguments().Return(csvStream);
			var trackFeedReader = new GenericFeedReader(feedUnpacker);

			var readFromFeeds = trackFeedReader.ReadIntoList<Track>(trackFullFeed);

			Assert.That(readFromFeeds.Count(), Is.EqualTo(2));

			csvStream.Dispose();
		}

		[Test]
		public void TestName()
		{
			IEnumerable<object> readIntoList = ReadIntoList(new Feed(FeedType.Full, FeedCatalogueType.Artist));
		}

		public IEnumerable<object> ReadIntoList(Feed feed)
		{
			var type = typeof(CsvDeserialize);
			var methodInfo = type.GetMethod("DeSerialize", new Type[]{typeof(Stream)});

			//return CsvDeserialize.DeSerialize<T>(decompressedStream);
			//return CsvDeserialize.DeSerialize<T>(decompressedStream);
			return null;
		}

		private static Type GetFeedType(Feed feed)
		{
			if (feed.CatalogueType == FeedCatalogueType.Artist && feed.FeedType == FeedType.Full)
			{
				return typeof(Artist);
			}
			if (feed.CatalogueType == FeedCatalogueType.Artist && feed.FeedType == FeedType.Incremental)
			{
				return typeof(ArtistIncremental);
			}
			if (feed.CatalogueType == FeedCatalogueType.Release && feed.FeedType == FeedType.Full)
			{
				return typeof(Release);
			}
			if (feed.CatalogueType == FeedCatalogueType.Release && feed.FeedType == FeedType.Incremental)
			{
				return typeof(ReleaseIncremental);
			}
			if (feed.CatalogueType == FeedCatalogueType.Track && feed.FeedType == FeedType.Full)
			{
				return typeof(Track);
			}
			return typeof(TrackIncremental);
		}
	}
}