using System;
using System.Collections.Generic;
using System.Text;
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

		public string SignUrlForLatestFeed(FeedCatalogueType feedCatalogueType, FeedType type, string countryCode)
		{
			RequireString("countryCode", countryCode);

			var endpoint = string.Format(FEED_ENDPOINT, feedCatalogueType.ToString().ToLower(), type.ToString().ToLower());

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
				{ "country", countryCode},
				{ "date", FeedsDateCreation.GetCurrentFullFeedDate(DateTime.Now)} // TODO - re card # 
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

	public enum FeedCatalogueType
	{
		Artist,
		Release,
		Track
	}

	public static class DictionaryExtensions
	{
		public static string ToQueryString(this IDictionary<string, string> collection)
		{
			var sb = new StringBuilder();
			foreach (var key in collection.Keys)
			{
				var parameter = OAuthTools.UrlEncodeStrict(collection[key]);
				sb.AppendFormat("{0}={1}&", key, parameter);
			}
			return sb.ToString().TrimEnd('&');
		}
	}
}