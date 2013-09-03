using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.FeedReader.Configuration
{
	public class ApiUrl : IApiUri
	{
		public string Uri {
			get { return "http://feeds.api.7digital.com/1.2"; } 
		}

		public string SecureUri {
			get { return "https://feeds.api.7digital.com/1.2"; } 
		}
	}
}