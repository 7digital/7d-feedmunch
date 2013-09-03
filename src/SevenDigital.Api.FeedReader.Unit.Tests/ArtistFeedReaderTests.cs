using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds.Artist;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class ArtistFeedReaderTests
	{
		[Test]
		public void Can_read_from_stub_feed()
		{
			var artistCsvStream = TestData.GetArtistCsvStream();
			var artistFeedFetcher = MockRepository.GenerateStub<IFeedUnpacker>();

			artistFeedFetcher.Stub(x => x.GetDecompressedStream()).IgnoreArguments().Return(artistCsvStream);
			var artistFeedReader = new ArtistFeedReader(artistFeedFetcher);

			var readFromFeeds = artistFeedReader.ReadIntoList();

			Assert.That(readFromFeeds.Count(), Is.EqualTo(3));

			artistCsvStream.Dispose();
		}
	}
}