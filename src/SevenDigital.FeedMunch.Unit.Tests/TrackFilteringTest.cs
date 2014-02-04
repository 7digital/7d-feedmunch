using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SevenDigital.Api.FeedReader.Feeds.Schema;

namespace SevenDigital.FeedMunch.Unit.Tests
{
	[TestFixture]
	public class TrackFilteringTests
	{
		private IEnumerable<Track> _tracks;

		[SetUp]
		public void SetUp()
		{
			_tracks = Tracks();
		}

		[Test]
		public void Can_find_row_with_single_value_if_exists()
		{
			var filter = new Filter("title=trackA");

			var filtered = _tracks.Where(filter.ApplyToRow).ToList();

			Assert.That(filtered.Count, Is.EqualTo(1));
			Assert.That(filtered[0].title, Is.EqualTo("trackA"));
		}

		[Test]
		public void Can_find_row_with_range_of_values()
		{
			var filter = new Filter("licensorID=1,2");

			var filtered = _tracks.Where(filter.ApplyToRow).ToList();

			Assert.That(filtered.Count, Is.EqualTo(3));
			Assert.That(filtered[0].title, Is.EqualTo("trackA"));
			Assert.That(filtered[1].title, Is.EqualTo("trackB"));
			Assert.That(filtered[2].title, Is.EqualTo("trackC"));
		}

		[Test]
		public void Can_find_row_with_negative_single_value()
		{
			var filter = new Filter("licensorID!=1");

			var filtered = _tracks.Where(filter.ApplyToRow).ToList();

			Assert.That(filtered.Count, Is.EqualTo(2));
			Assert.That(filtered[0].title, Is.EqualTo("trackC"));
			Assert.That(filtered[1].title, Is.EqualTo("trackD"));
		}

		[Test]
		public void Can_find_row_with_negative_range_of_values()
		{
			var filter = new Filter("licensorID!=1,2");

			var filtered = _tracks.Where(filter.ApplyToRow).ToList();

			Assert.That(filtered.Count, Is.EqualTo(1));
			Assert.That(filtered[0].title, Is.EqualTo("trackD"));
		}

		private static IEnumerable<Track> Tracks()
		{
			var trackA = new Track
			{
				title = "trackA",
				licensorID = 1
			};

			var trackB = new Track
			{
				title = "trackB",
				licensorID = 1
			};

			var trackC = new Track
			{
				title = "trackC",
				licensorID = 2
			};

			var trackD = new Track
			{
				title = "trackD",
				licensorID = 3
			};


			var tracks = new List<Track>
			{
				trackA,
				trackB,
				trackC,
				trackD
			};
			return tracks;
		}
	}
}