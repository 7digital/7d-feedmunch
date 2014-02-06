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
			_feedsUrlCreator.Stub(x => x.SignUrlForLatestFeed(FeedCatalogueType.Track, FeedType.Full, "GB")).IgnoreArguments().Return("http://localhost");
			_fileHelper = MockRepository.GenerateStub<IFileHelper>();
			_fileHelper.Stub(x => x.BuildFullFilepath(null)).IgnoreArguments().Return("testFolderName");
		}

		[Test]
		public void Should_set_filename_and_url()
		{
			var feedDownload = new FeedDownload(_feedsUrlCreator, _fileHelper);

			var suppliedFeed = new Feed(FeedType.Full, FeedCatalogueType.Track);

			var downloadToStream = feedDownload.DownloadToStream(suppliedFeed).Result;

			Assert.That(feedDownload.CurrentFileName, Is.EqualTo("testFolderName"));
			Assert.That(feedDownload.CurrentSignedUrl, Is.EqualTo("http://localhost"));

			Assert.That(downloadToStream, Is.Not.Null);
		}
	}
}
