using System;
using System.IO;
using System.Linq;
using System.Web;

namespace SevenDigital.FeedMunch.Configuration
{
	public class OAuthConsumerCreds
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

		public static OAuthConsumerCreds GenerateFromFile(string filepath)
		{
			if (HttpContext.Current != null)
			{
				filepath = HttpContext.Current.Server.MapPath("~/" + filepath);
			}

			if (!File.Exists(filepath))
			{
				throw new ArgumentException(string.Format("Application expectes a file at {0} containing valid api consumer credentials", filepath));
			}

			var allLines = File.ReadAllLines(filepath).Select(x=>x.Trim()).Where(x=>x!="").ToArray();

			if (allLines.Length != 2)
			{
				throw new ArgumentException("Credentials at filepath are invalid");
			}

			return new OAuthConsumerCreds(allLines[0], allLines[1]);
		}
	}
}