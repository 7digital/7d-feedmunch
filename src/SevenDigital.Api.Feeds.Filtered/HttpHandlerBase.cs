using System.Web;

namespace SevenDigital.Api.Feeds.Filtered
{
	public abstract class HttpHandlerBase : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			ProcessRequest(new HttpContextWrapper(context));
		}

		public abstract void ProcessRequest(HttpContextBase context);

		public bool IsReusable { get { return true; } }
	}
}