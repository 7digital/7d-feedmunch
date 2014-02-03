using System;
using SevenDigital.Api.FeedReader.Dates;

namespace SevenDigital.Api.FeedReader
{
	public abstract class Feed
	{
		private string _countryCode = "GB";
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;
		
		public string GetLatest()
		{
			var feedsDate = GetPreviousFeedDate();
			return feedsDate + "-" + CountryCode.ToLower() + "-" + GetCatalogueType().ToString().ToLower() + "-" + GetFeedType().ToString().ToLower() + "-feed.gz";
		}

		public abstract FeedCatalogueType GetCatalogueType();
		public abstract FeedType GetFeedType();

		public string CountryCode
		{
			get { return _countryCode; }
			set { _countryCode = value; }
		}

		public FeedWriteMethod WriteMethod { get; set; }

		private string GetPreviousFeedDate()
		{
			return GetFeedType() == FeedType.Full ? GetPreviousFullFeedDate() : GetPreviousIncrementalFeedDate();
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