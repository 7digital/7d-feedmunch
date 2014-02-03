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
			const string expected = "feeds";
			var feedsFileHelper = new FeedsFileHelper(expected, "");
			var actual = feedsFileHelper.GetOrCreateFeedsFolder();
			Assert.That(actual, Is.StringContaining(expected));
			Assert.That(Directory.Exists(actual));
		}

		[Test]
		public void Gets_and_creates_correct_output_folder()
		{
			const string expected = "output";
			var feedsFileHelper = new FeedsFileHelper("", expected);
			var actual = feedsFileHelper.GetOrCreateOutputFolder("");
			Assert.That(actual, Is.StringContaining(expected));
			Assert.That(Directory.Exists(actual));
		}
	}
}