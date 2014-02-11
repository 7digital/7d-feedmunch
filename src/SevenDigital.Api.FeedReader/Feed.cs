using System;

namespace SevenDigital.Api.FeedReader
{
	public class Feed
	{
		private readonly FeedType _type;
		private readonly FeedCatalogueType _catalogueType;
		private string _country = "GB";
		public const DayOfWeek FULL_FEED_DAY_OF_WEEK = DayOfWeek.Monday;

		public Feed(FeedType type, FeedCatalogueType catalogueType)
		{
			_type = type;
			_catalogueType = catalogueType;
		}

		public string Country
		{
			get { return _country; }
			set { _country = value; }
		}

		public FeedType FeedType { get { return _type; } }

		public FeedCatalogueType CatalogueType
		{
			get { return _catalogueType; }
		}

		public string ExistingPath { get; set; }

		public override string ToString()
		{
			return string.Format("FeedType: {0} FeedCatalogue: {1} Country: {2} Date: {3}", FeedType, CatalogueType, Country );
		}
	}
}