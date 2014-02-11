using System;
using System.Collections.Generic;
using OAuth;
using SevenDigital.Api.FeedReader.Configuration;

namespace SevenDigital.Api.FeedReader
{
	public class FeedsUrlCreator : IFeedsUrlCreator
	{
		private readonly ApiUrl _apiUrl;
		private readonly OAuthConsumerCreds _oauthConsumerCreds;

		private const string FEED_ENDPOINT = "/feed/{0}/{1}";

		public FeedsUrlCreator(ApiUrl apiUrl, OAuthConsumerCreds oauthConsumerCreds)
		{
			_apiUrl = apiUrl;
			_oauthConsumerCreds = oauthConsumerCreds;
		}

		public string SignUrlForFeed(Feed feed)
		{
			RequireString("countryCode", feed.Country);

			var endpoint = string.Format(FEED_ENDPOINT, feed.CatalogueType.ToString().ToLower(), feed.FeedType.ToString().ToLower());

			var requestUrl = string.Concat(_apiUrl.Uri, endpoint);
			var oAuthRequest = new OAuthRequest
			{
				Type = OAuthRequestType.ProtectedResource,
				RequestUrl = requestUrl,
				Method = "GET",
				ConsumerKey = _oauthConsumerCreds.ConsumerKey,
				ConsumerSecret = _oauthConsumerCreds.ConsumerSecret
			};

			var parameters = new Dictionary<string,string>
			{
				{ "country", feed.Country},
				{ "date", FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, feed.FeedType)}
			};

			var authorizationQuery = oAuthRequest.GetAuthorizationQuery(parameters);
			return string.Format("{0}?{1}{2}", requestUrl, authorizationQuery, parameters.ToQueryString());
		}

		private static void RequireString(string paramName, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(paramName);
			}
		}
	}
}