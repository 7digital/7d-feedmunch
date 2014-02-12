using System;
using System.Linq;
using System.Net;
using NUnit.Framework;
using RestSharp;
using SevenDigital.Api.FeedReader;

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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Full);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_artist_full_" + currentFeedDate + "-filtered.gz\""));
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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Updates);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_artist_updates_" + currentFeedDate + "-filtered.gz\""));
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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Full);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_track_full_" + currentFeedDate + "-filtered.gz\""));
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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Updates);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_track_updates_" + currentFeedDate + "-filtered.gz\""));
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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Full);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_release_full_" + currentFeedDate + "-filtered.gz\""));
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

			var currentFeedDate = FeedsDateCreation.GetCurrentFeedDate(DateTime.Now, FeedType.Updates);

			Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(restResponse.ContentType, Is.EqualTo("application/x-gzip"));
			Assert.That(restResponse.Headers.Single(x => x.Name == "Content-disposition").Value, Is.EqualTo("attachment; filename=\"GB_release_updates_" + currentFeedDate + "-filtered.gz\""));
		}
	}
}
