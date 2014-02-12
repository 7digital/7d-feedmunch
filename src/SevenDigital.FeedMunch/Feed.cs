using System;

namespace SevenDigital.FeedMunch
{
	public class Feed
	{
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;
		private readonly FeedType _type;
		private readonly FeedCatalogueType _catalogueType;
		private readonly string _country = "GB";
		private readonly DateTime _date;

		public Feed(FeedType type, FeedCatalogueType catalogueType, string country, DateTime date)
		{
			_type = type;
			_catalogueType = catalogueType;
			_country = country;
			_date = date;
		}

		public Feed(FeedType type, FeedCatalogueType catalogueType, string country)
			:this(type, catalogueType, country, DateTime.Now) 
		{}

		public FeedType FeedType { get { return _type; } }
		public FeedCatalogueType CatalogueType { get { return _catalogueType; } }
		public DateTime Date { get { return _date; } }
		public string Country { get { return _country; } }

		public string ExistingPath { get; set; }

		public override string ToString()
		{
			return string.Format("FeedType: {0} FeedCatalogue: {1} Country: {2} Date: {3}", FeedType, CatalogueType, Country, GetFeedDate());
		}

		public string GetFeedDate()
		{
			return FeedsDateCreation.GetCurrentFeedDate(Date, FeedType);
		}
	}
}