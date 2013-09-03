using System;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Wrapper.EndpointResolution.OAuth;

namespace SevenDigital.Api.FeedReader
{
	public class FeedsUrlCreator : IFeedsUrlCreator
	{
		private readonly IUrlSigner _urlSigner;
		private readonly IApiUri _apiUrl;
		private readonly IOAuthCredentials _oauthConsumerCreds;

		private const string ARTIST_FEED_ENDPOINT = "/feed/artist/{0}";
		private const string REQUIRED_PARAMS_QUERYSTRING = "?country={0}&date={1}";

		public FeedsUrlCreator(IUrlSigner urlSigner, IApiUri apiUrl, IOAuthCredentials oauthConsumerCreds )
		{
			_urlSigner = urlSigner;
			_apiUrl = apiUrl;
			_oauthConsumerCreds = oauthConsumerCreds;
		}

		public string SignUrlForLatestArtistFeed(FeedType type, string countryCode)
		{
			RequireString("countryCode", countryCode);

			var endpoint = string.Format(ARTIST_FEED_ENDPOINT, type.ToString().ToLower());
			var querystring = string.Format(REQUIRED_PARAMS_QUERYSTRING, countryCode, FeedsHelper.GetPreviousFullFeedDate());
			var url = string.Concat(_apiUrl.Uri, endpoint, querystring);

			return _urlSigner.SignGetUrl(url, "", "", _oauthConsumerCreds);
		}

		private static void RequireString(string paramName, string value)
		{
			if(string.IsNullOrEmpty(value))
				throw new ArgumentNullException(paramName);
		}
	}
}