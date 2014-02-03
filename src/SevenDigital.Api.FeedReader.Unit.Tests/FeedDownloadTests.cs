using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds;
using SevenDigital.Api.FeedReader.Feeds.Artist;
using SevenDigital.Api.FeedReader.Http;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class FeedDownloadTests
	{
		private IFeedsUrlCreator _feedsUrlCreator;
		private IFileHelper _fileHelper;
		private IWebClientWrapper _webClient;

		[SetUp]
		public void SetUp()
		{
			_feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();
			_fileHelper = MockRepository.GenerateStub<IFileHelper>();
			_fileHelper.Stub(x => x.GetOrCreateFeedsFolder()).Return("testFolderName");
			_webClient = MockRepository.GenerateStub<IWebClientWrapper>();
		}

		[Test]
		public void _returns_true_if_feed_exists()
		{
			var artistFeed = AvailableFeeds.ArtistFull;
			_fileHelper.Stub(x => x.FeedExists(artistFeed)).Return(true);

			var artistFeedDownload = new FeedDownload(_feedsUrlCreator, _webClient, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed));
		}

		[Test]
		public void _returns_false_if_feed_doesnt_exists()
		{
			var artistFeed = AvailableFeeds.ArtistFull;
			_fileHelper.Stub(x => x.FeedExists(artistFeed)).Return(false);
			var artistFeedDownload = new FeedDownload(_feedsUrlCreator, _webClient, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed), Is.False);
		}

		[Test]
		public void _tries_to_save_if_feed_does_not_exist()
		{
			const string expectedSignedFeedsUrl = "testSignedUrl";

			var artistFeed = AvailableFeeds.ArtistFull;
			_fileHelper.Stub(x => x.FeedExists(artistFeed)).Return(true);

			var feedsUrlCreator = MockRepository.GenerateStub<IFeedsUrlCreator>();
			feedsUrlCreator.Stub(x => x.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "34")).Return(expectedSignedFeedsUrl);

			var webClientWrapper = MockRepository.GenerateStub<IWebClientWrapper>();

			var fileHelper = MockRepository.GenerateStub<IFileHelper>();
			fileHelper.Stub(x=>x.GetOrCreateFeedsFolder()).Return("feeds");

			var artistFeedDownload = new FeedDownload(feedsUrlCreator, webClientWrapper, fileHelper);
			artistFeedDownload.SaveLocally(artistFeed);

			Assert.That(artistFeedDownload.CurrentSignedUrl, Is.EqualTo(expectedSignedFeedsUrl));

			feedsUrlCreator.AssertWasCalled(x => x.SignUrlForLatestFeed(FeedCatalogueType.Artist, FeedType.Full, "34"));

			webClientWrapper.AssertWasCalled(x => x.DownloadFile(Arg<string>.Is.Equal(expectedSignedFeedsUrl), Arg<string>.Is.Anything));
		}
	}
}
