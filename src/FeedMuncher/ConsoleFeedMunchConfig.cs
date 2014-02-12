using System.ComponentModel;
using SevenDigital.FeedMunch;

namespace FeedMuncher
{
	public class ConsoleFeedMunchConfig : FeedMunchConfig
	{
		private string _output;

		[Description("")]
		public string Output
		{
			get { return BuildDefaultOutputFilename(); }
			set { _output = value; }
		}

		private string BuildDefaultOutputFilename()
		{
			if (string.IsNullOrEmpty(_output))
			{
				var isFiltered = !string.IsNullOrEmpty(Filter) ? "-filtered" : string.Empty;
				return string.Format("./{0}-{1}-{2}-{3}-feed{4}", Date, Country, Catalog.ToString().ToLower(), Feed.ToString().ToLower(), isFiltered);
			}

			return _output;
		}
	}
}