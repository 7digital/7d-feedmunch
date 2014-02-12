using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using SevenDigital.FeedMunch;

namespace SevenDigital.Api.Feeds.Filtered
{
	public static class UriToFeedMunchConfigMapper
	{
		public static FeedMunchConfig ToFeedMunchConfig(this Uri uri)
		{
			var segments = new Stack(uri.Segments);
			var feedType = ((string)segments.Pop()).TrimEnd('/');
			var catalogType = ((string)segments.Pop()).TrimEnd('/');

			var queryStringDictionary = uri.QueryStringDictionary();
			var country = queryStringDictionary.ContainsKey("country") ? queryStringDictionary["country"] : "GB";
			var filter = queryStringDictionary.ContainsKey("filter") ? HttpUtility.UrlDecode(queryStringDictionary["filter"]) : null;
			
			return new FeedMunchConfig
			{
				Catalog = (FeedCatalogueType)Enum.Parse(typeof(FeedCatalogueType), catalogType, true),
				Country = country,
				Feed = (FeedType)Enum.Parse(typeof(FeedType), feedType, true),
				Filter = filter
			};
		}

		public static IDictionary<string,string> QueryStringDictionary(this Uri uri)
		{
			var query = uri.Query.TrimStart('?');
			var pairs = query.Split(new [] {"&"}, StringSplitOptions.RemoveEmptyEntries);

			var dictionary = new Dictionary<string, string>();

			foreach (var pair in pairs)
			{
				var kvp = pair.Split('=');
				var key = kvp[0];
				var value = kvp[1];

				dictionary.Add(key, value);
			}

			return dictionary;
		}
	}
}