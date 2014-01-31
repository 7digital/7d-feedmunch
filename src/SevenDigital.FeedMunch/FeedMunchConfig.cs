using SevenDigital.Api.FeedReader;

namespace SevenDigital.FeedMunch
{
	public class FeedMunchConfig
	{
		public FeedType FeedType { get; set; }
		public FeedCatalogueType FeedCatalogue { get; set; }
		public string Filter { get; set; }
		public string LocalFilePath { get; set; }
		public int ShopId { get; set; }
	}
}