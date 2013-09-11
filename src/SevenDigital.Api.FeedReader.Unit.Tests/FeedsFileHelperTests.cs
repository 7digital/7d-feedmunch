using System;
using System.IO;
using NUnit.Framework;

namespace SevenDigital.Api.FeedReader.Unit.Tests
{
	[TestFixture]
	public class FeedsFileHelperTests
	{
		[Test]
		public void Gets_and_creates_correct_feeds_folder()
		{
			const string testFeedsFOlder = "feeds";
			var feedsFileHelper = new FeedsFileHelper(testFeedsFOlder);
			var expected = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), testFeedsFOlder);
			TryDeleteDirectory(expected);

			var orCreateFeedsDirectory = feedsFileHelper.GetOrCreateFeedsFolder();
			Assert.That(orCreateFeedsDirectory, Is.EqualTo(expected));
			Assert.That(Directory.Exists(expected));
		}

		private static void TryDeleteDirectory(string expected)
		{
			if (Directory.Exists(expected))
			{
				Directory.Delete(expected, true);
			}
		}
	}
}