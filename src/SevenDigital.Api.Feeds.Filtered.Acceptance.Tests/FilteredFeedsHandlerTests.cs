using System.Linq;
using System.Net;
using NUnit.Framework;
using RestSharp;

namespace SevenDigital.Api.Feeds.Filtered.Acceptance.Tests
{
	[TestFixture]
	public class FilteredFeedsHandlerTests
	{
		private RestClient _restClient;

		[SetUp]
		public void SetUp()
		{
			_restClient = new RestClient(Config.ServiceUrl);
		}

		[Test]
		public void Can_download_artist_full_filtered()
		{
			var restRequest = new RestRequest("artist/full");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "name=Interpol"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x=>x.Name=="Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-artist-full-feed-filtered.gz\""));
		}

		[Test]
		public void Can_download_artist_updates_filtered()
		{
			var restRequest = new RestRequest("artist/updates");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "name=Interpol"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-artist-updates-feed-filtered.gz\""));
		}

		[Test]
		public void Can_download_track_full_filtered()
		{
			var restRequest = new RestRequest("track/full");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "version=Album Version"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-track-full-feed-filtered.gz\""));
		}

		[Test]
		public void Can_download_track_updates_filtered()
		{
			var restRequest = new RestRequest("track/updates");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "version=Album Version"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-track-updates-feed-filtered.gz\""));
		}

		[Test]
		public void Can_download_release_full_filtered()
		{
			var restRequest = new RestRequest("release/full");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "licensorId=1"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-release-full-feed-filtered.gz\""));
		}

		[Test]
		public void Can_download_release_updates_filtered()
		{
			var restRequest = new RestRequest("release/updates");
			restRequest.AddParameter(new Parameter
			{
				Name = "filter",
				Type = ParameterType.QueryString,
				Value = "licensorId=1"
			});
			var restResponse = _restClient.Head(restRequest);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"20140203-GB-release-updates-feed-filtered.gz\""));
		}
	}
}
