using System.Net;
using NUnit.Framework;
using RestSharp;

namespace SevenDigital.Api.Feeds.Filtered.Acceptance.Tests
{
	[TestFixture]
	public class FilteredFeedsHandlerTests
	{
		[Test]
		public void Can_browse_to_artist_full()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("artist/full");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am artist full"));
		}

		[Test]
		public void Can_browse_to_track_full()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("track/full");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am track full"));
		}

		[Test]
		public void Can_browse_to_release_full()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("release/full");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am release full"));
		}

		[Test]
		public void Can_browse_to_artist_update()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("artist/updates");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am artist updates"));
		}

		[Test]
		public void Can_browse_to_track_update()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("track/updates");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am track updates"));
		}

		[Test]
		public void Can_browse_to_release_update()
		{
			var restClient = new RestClient("http://localhost/7d-feeds-filtered/");
			var restRequest = new RestRequest("release/updates");

			var restResponse = restClient.Get(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.Content, Is.EqualTo("I am release updates"));
		}
	}
}
