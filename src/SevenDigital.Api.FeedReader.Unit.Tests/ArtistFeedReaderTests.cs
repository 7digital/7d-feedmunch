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
			var artistCsvStream = ArtistData.GetArtistCsvStream();
			var artistFeedFetcher = MockRepository.GenerateStub<IFeedUnpacker>();

			artistFeedFetcher.Stub(x => x.GetDecompressedStream()).IgnoreArguments().Return(artistCsvStream);
			var artistFeedReader = new ArtistFeedReader(artistFeedFetcher);

			var readFromFeeds = artistFeedReader.ReadIntoList();

			Assert.That(readFromFeeds.Count(), Is.EqualTo(3));

			artistCsvStream.Dispose();
		}
	}

	//[TestFixture]
	//public class TrackFeedReaderTests
	//{
	//	[Test]
	//	public void Can_read_from_stub_feed()
	//	{
	//		var artistCsvStream = TrackData.GetCsvStream();
	//		var artistFeedFetcher = MockRepository.GenerateStub<IFeedUnpacker>();

	//		artistFeedFetcher.Stub(x => x.GetDecompressedStream()).IgnoreArguments().Return(artistCsvStream);
	//		var artistFeedReader = new TrackFeedReader(artistFeedFetcher);

	//		var readFromFeeds = artistFeedReader.ReadIntoList();

	//		Assert.That(readFromFeeds.Count(), Is.EqualTo(3));

	//		artistCsvStream.Dispose();
	//	}
	//}
}