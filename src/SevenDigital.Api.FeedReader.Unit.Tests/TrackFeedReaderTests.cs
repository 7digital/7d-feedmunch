using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds.Track;
using SevenDigital.Api.FeedReader.Unit.Tests.TestData;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class TrackFeedReaderTests
	{
		[Test]
		public void Can_read_from_stub_feed()
		{
			var csvStream = TrackData.GetCsvStream();
			var feedUnpacker = MockRepository.GenerateStub<IFeedUnpacker>();

			feedUnpacker.Stub(x => x.GetDecompressedStream(new TrackFullFeed())).IgnoreArguments().Return(csvStream);
			var artistFeedReader = new TrackFeedReader(feedUnpacker);

			var readFromFeeds = artistFeedReader.ReadIntoList();

			Assert.That(readFromFeeds.Count(), Is.EqualTo(2));

			csvStream.Dispose();
		}
	}
}