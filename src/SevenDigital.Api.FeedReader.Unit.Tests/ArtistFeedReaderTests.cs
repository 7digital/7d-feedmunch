using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds.Artist;
using SevenDigital.Api.FeedReader.Unit.Tests.TestData;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class ArtistFeedReaderTests
	{
		[Test]
		public void Can_read_from_stub_feed()
		{
			var artistCsvStream = ArtistData.GetCsvStream();
			var artistFeedFetcher = MockRepository.GenerateStub<IFeedUnpacker>();

			var artistFullFeed = new ArtistFullFeed();
			artistFeedFetcher.Stub(x => x.GetDecompressedStream(artistFullFeed)).IgnoreArguments().Return(artistCsvStream);
			var artistFeedReader = new ArtistFeedReader(artistFeedFetcher);

			var readFromFeeds = artistFeedReader.ReadIntoList(artistFullFeed);

			Assert.That(readFromFeeds.Count(), Is.EqualTo(3));

			artistCsvStream.Dispose();
		}
	}
}