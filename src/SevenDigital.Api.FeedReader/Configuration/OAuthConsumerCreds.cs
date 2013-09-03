using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.FeedReader.Configuration
{
	public class OAuthConsumerCreds : IOAuthCredentials
	{
		private readonly string _key;
		private readonly string _secret;

		public OAuthConsumerCreds(string key, string secret)
		{
			_key = key;
			_secret = secret;
		}

		public string ConsumerKey
		{
			get {return _key; }
		}

		public string ConsumerSecret
		{
			get { return _secret; }
		}
	}
}