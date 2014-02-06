using System.Web;
using NUnit.Framework;
using Rhino.Mocks;

namespace SevenDigital.Api.Feeds.Filtered.Unit.Tests
{
	[TestFixture]
	public class MyTestFixture
	{
		[Test]
		public void TestName()
		{
			var filteredFeedHandler = new FilteredFeedHandler();

			var httpContextBase = MockRepository.GenerateStub<HttpContextBase>();
			var httpRequestBase = MockRepository.GenerateStub<HttpRequestBase>();
			httpContextBase.Stub(x=>x.Request).Return(httpRequestBase);

			filteredFeedHandler.ProcessRequest(httpContextBase);


		}
	}
}
