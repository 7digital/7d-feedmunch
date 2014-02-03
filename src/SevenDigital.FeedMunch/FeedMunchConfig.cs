using System.ComponentModel;
using SevenDigital.Api.FeedReader;

namespace SevenDigital.FeedMunch
{
	public class FeedMunchConfig
	{
		public FeedMunchConfig()
		{
			Shop = 34;
			Output = "./tempfile.tmp";
		}

		[Description("Either Full or Incremental")]
		public FeedType Feed { get; set; }
		
		[Description("Either Artist, Release or Track")]
		public FeedCatalogueType Catalog { get; set; }
		
		[Description("e.g. \"licensorId != 1\", CurrentCultureIgnoreCase")]
		public string Filter { get; set; }

		[Description("")]
		public string Output { get; set; }

		[Description("")]
		public int Shop { get; set; }
	}
}