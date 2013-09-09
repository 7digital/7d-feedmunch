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
			var feedsDate = GetPreviousIncrementalFeedDate();
			return feedsDate + "-" + CountryCode.ToLower() + "-" + FeedCatalogueType().ToString().ToLower() + "-" + FeedType().ToString().ToLower() + "-feed.gz";
		}

		public abstract FeedCatalogueType FeedCatalogueType();
		public abstract FeedType FeedType();

		public string CountryCode
		{
			get { return _countryCode; }
			set { _countryCode = value; }
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
}