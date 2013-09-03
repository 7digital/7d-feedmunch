using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds.Artist;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class ArtistFeedDownloadTests
	{
		[Test]
		public void _returns_true_if_feed_exists()
		{
			var artistFeed = ArtistFeedThatExists();
			var artistFeedDownload = new ArtistFeedDownload(null, null, artistFeed);
			Assert.That(artistFeedDownload.FeedAlreadyExists());
		}

		[Test]
		public void _does_not_try_to_save_if_feed_exists()
		{
			var artistFeed = ArtistFeedThatExists();

			var feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();

			var artistFeedDownload = new ArtistFeedDownload(feedsUrlCreator, null, artistFeed);
			artistFeedDownload.SaveLocally();

			feedsUrlCreator.AssertWasNotCalled(x => x.SignUrlForLatestArtistFeed(FeedType.Full, "GB"));
		}

		[Test]
		public void _returns_false_if_feed_doesnt_exists()
		{
			var artistFeed = ArtistFeedThatDoesNotExist();
			var artistFeedDownload = new ArtistFeedDownload(null, null, artistFeed);
			Assert.That(artistFeedDownload.FeedAlreadyExists(), Is.False);
		}

		[Test]
		public void _tries_to_save_if_feed_does_not_exist()
		{
			const string expectedSignedFeedsUrl = "testSignedUrl";

			var artistFeed = ArtistFeedThatDoesNotExist();

			var feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();
			feedsUrlCreator.Stub(x => x.SignUrlForLatestArtistFeed(FeedType.Full, "GB")).Return(expectedSignedFeedsUrl);

			var webClientWrapper = MockRepository.GenerateStub<IWebClientWrapper>();

			var artistFeedDownload = new ArtistFeedDownload(feedsUrlCreator, webClientWrapper, artistFeed);
			artistFeedDownload.SaveLocally();

			feedsUrlCreator.AssertWasCalled(x => x.SignUrlForLatestArtistFeed(FeedType.Full, "GB"));

			webClientWrapper.AssertWasCalled(x => x.DownloadFile(expectedSignedFeedsUrl, artistFeed.GetLatest()));
		}

		private static Feed ArtistFeedThatExists()
		{
			var artistFeed = MockRepository.GenerateStub<Feed>();
			artistFeed.Stub(x => x.GetLatest()).Return(Path.GetTempFileName());
			return artistFeed;
		}

		private static Feed ArtistFeedThatDoesNotExist()
		{
			var artistFeed = MockRepository.GenerateStub<Feed>();
			artistFeed.Stub(x => x.GetLatest()).Return(Path.GetTempPath() + "/fakefile.txt");
			return artistFeed;
		}
	}
}
