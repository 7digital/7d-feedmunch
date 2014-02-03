using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds;
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

			var trackFullFeed = AvailableFeeds.TrackFull;
			feedUnpacker.Stub(x => x.GetDecompressedStream(trackFullFeed)).IgnoreArguments().Return(csvStream);
			var trackFeedReader = new TrackFeedReader(feedUnpacker);

			var readFromFeeds = trackFeedReader.ReadIntoList(trackFullFeed);

			Assert.That(readFromFeeds.Count(), Is.EqualTo(2));

			csvStream.Dispose();
		}
	}
}