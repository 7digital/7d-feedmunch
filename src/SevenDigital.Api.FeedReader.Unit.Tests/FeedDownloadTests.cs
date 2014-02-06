using NUnit.Framework;
using Rhino.Mocks;
using SevenDigital.Api.FeedReader.Feeds;

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
			var artistFeed = AvailableFeeds.ArtistFull;
			_fileHelper.Stub(x => x.FeedExists(artistFeed)).Return(true);

			var artistFeedDownload = new FeedDownload(_feedsUrlCreator, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed));
		}

		[Test]
		public void _returns_false_if_feed_doesnt_exists()
		{
			var artistFeed = AvailableFeeds.ArtistFull;
			_fileHelper.Stub(x => x.FeedExists(artistFeed)).Return(false);
			var artistFeedDownload = new FeedDownload(_feedsUrlCreator, _fileHelper);
			Assert.That(artistFeedDownload.FeedAlreadyExists(artistFeed), Is.False);
		}
	}
}
