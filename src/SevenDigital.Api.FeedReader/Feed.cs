using System;
using SevenDigital.Api.FeedReader.Dates;

namespace SevenDigital.Api.FeedReader
{
	public class Feed
	{
		private readonly FeedType _type;
		private readonly FeedCatalogueType _catalogueType;
		private string _countryCode = "34";
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;

		public Feed(FeedType type, FeedCatalogueType catalogueType)
		{
			_type = type;
			_catalogueType = catalogueType;
		}

		public string CountryCode
		{
			get { return _countryCode; }
			set { _countryCode = value; }
		}

		public FeedWriteMethod WriteMethod { get; set; }
		public FeedType FeedType { get { return _type; } }
		public FeedCatalogueType CatalogueType { get { return _catalogueType; } }


		public string GetLatest()
		{
			var feedsDate = GetPreviousFeedDate();
			return feedsDate + "-" + CountryCode.ToLower() + "-" + _catalogueType.ToString().ToLower() + "-" + _type.ToString().ToLower() + "-feed.gz";
		}

		private string GetPreviousFeedDate()
		{
			return _type == FeedType.Full ? GetPreviousFullFeedDate() : GetPreviousIncrementalFeedDate();
		}

		protected string GetPreviousFullFeedDate()
		{
			return DateTime.Now.PreviousDayOfWeek(FULL_FEED_DAY_OF_WEEK).ToString("yyyyMMdd");
		}

		protected string GetPreviousIncrementalFeedDate()
		{
			return DateTime.Now.PreviousDayOfWeek().ToString("yyyyMMdd");
		}
	}

	public enum FeedWriteMethod
	{
		ResumeIfExists = 0, // Resumes feed download as if feed is a partial
		ForceOverwriteIfExists = 1, // Overwrites existing feed if feed filename the same
		IgnoreIfExists = 2 // Ignores download existing feed found if filename the same
	}
}