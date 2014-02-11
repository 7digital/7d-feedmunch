using System.ComponentModel;
using SevenDigital.Api.FeedReader;

namespace SevenDigital.FeedMunch
{
	public class FeedMunchConfig
	{
		public FeedMunchConfig()
		{
			Country = "GB";
			Output = "./tempfile.tmp";
		}

		[Description("Either Full or Updates")]
		public FeedType Feed { get; set; }
		
		[Description("Either Artist, Release or Track")]
		public FeedCatalogueType Catalog { get; set; }
		
		[Description("e.g. \"licensorId != 1\", CurrentCultureIgnoreCase")]
		public string Filter { get; set; }

		[Description("")]
		public string Output { get; set; }

		[Description("")]
		public string Country { get; set; }

		[Description("/limit allows you to specifiy a  number of rows to limit the download to, currently only 0-row number, defaults to 0 which is all rows")]
		public int Limit { get; set; }

		[Description("/existing allows you to specify a local gz feeds file")]
		public string Existing { get; set; }

		[Description("/date allows you to specify a date in the yyyyMMdd format")]
		public string Date { get; set; }
	}
}