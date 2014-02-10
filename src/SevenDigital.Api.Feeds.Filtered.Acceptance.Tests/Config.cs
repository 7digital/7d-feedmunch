using System.Configuration;

namespace SevenDigital.Api.Feeds.Filtered.Acceptance.Tests
{
	public static class Config
	{
		private static readonly string _serviceUrl;

		static Config()
		{
			_serviceUrl = ConfigurationManager.AppSettings["Service.Url"];
		}

		public static string ServiceUrl { get { return _serviceUrl; } }

	}
}