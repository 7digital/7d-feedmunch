using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class FeedDownloadTests
	{
		private IFeedsUrlCreator _feedsUrlCreator;
		private IFileHelper _fileHelper;

		[SetUp]
		public void SetUp()
		{
			_feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();
			_fileHelper = MockRepository.GenerateStub<IFileHelper>();
			_fileHelper.Stub(x => x.GetOrCreateFeedsFolder()).Return("testFolderName");
		}

		[Test]
		public void _returns_true_if_feed_exists()
		{
			var artistFeed = FeedThatExists();
			var artistFeedDownload = new FeedDownload(null, null, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed));
		}

		[Test]
		public void _does_not_try_to_save_if_feed_exists()
		{
			var artistFeed = FeedThatExists();

			var artistFeedDownload = new FeedDownload(_feedsUrlCreator, null, _fileHelper);
			artistFeedDownload.SaveLocally(artistFeed);

			_feedsUrlCreator.AssertWasNotCalled(x => x.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "GB"));
		}

		[Test]
		public void _returns_false_if_feed_doesnt_exists()
		{
			var artistFeed = FeedThatDoesNotExist();
			var artistFeedDownload = new FeedDownload(null, null, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed), Is.False);
		}

		[Test]
		public void _tries_to_save_if_feed_does_not_exist()
		{
			const string expectedSignedFeedsUrl = "testSignedUrl";

			var artistFeed = FeedThatDoesNotExist();

			var feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();
			feedsUrlCreator.Stub(x => x.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "GB")).Return(expectedSignedFeedsUrl);

			var webClientWrapper = MockRepository.GenerateStub<IWebClientWrapper>();

			var fileHelper = MockRepository.GenerateStub<IFileHelper>();
			fileHelper.Stub(x=>x.GetOrCreateFeedsFolder()).Return("feeds");

			var artistFeedDownload = new FeedDownload(feedsUrlCreator, webClientWrapper, fileHelper);
			artistFeedDownload.SaveLocally(artistFeed);

			feedsUrlCreator.AssertWasCalled(x => x.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "GB"));

			webClientWrapper.AssertWasCalled(x => x.DownloadFile(expectedSignedFeedsUrl, artistFeed.GetLatest()));
		}

		private static Feed FeedThatExists()
		{
			var artistFeed = MockRepository.GenerateStub<Feed>();
			artistFeed.Stub(x => x.GetLatest()).Return(Path.GetTempFileName());
			return artistFeed;
		}

		private static Feed FeedThatDoesNotExist()
		{
			var artistFeed = MockRepository.GenerateStub<Feed>();
			artistFeed.Stub(x => x.GetLatest()).Return(Path.GetTempPath() + "/fakefile.txt");
			return artistFeed;
		}
	}
}
