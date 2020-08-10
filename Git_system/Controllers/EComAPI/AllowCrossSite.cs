using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Git_system.Controllers.EComAPI
{
    public class AllowCrossSite : ActionFilterAttribute
    {
        public string hostname { get; set; }

        public AllowCrossSite()
        {
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request != null)
            {
                if (hostname == null)
                {
                    HttpRequestMessage httpRequestMessage = actionExecutedContext.Request;
                    if (httpRequestMessage.Headers.Referrer != null)
                    {
                        HttpRequestHeaders httpRequestHeaders = httpRequestMessage.Headers;
                        string host = httpRequestMessage.Headers.Referrer.Host;
                        int port = httpRequestMessage.Headers.Referrer.Port;
                        this.hostname = string.Format("{0}://{1}:{2}", httpRequestHeaders.Referrer.Scheme, httpRequestHeaders.Referrer.Host, httpRequestHeaders.Referrer.Port);
                    }
                    else
                    {
                        this.hostname = string.Format("{0}://{1}:{2}", httpRequestMessage.RequestUri.Scheme, httpRequestMessage.RequestUri.Host, httpRequestMessage.RequestUri.Port);
                    }
                }
                actionExecutedContext.Response.Headers.Remove("Access-Control-Allow-Origin");
                actionExecutedContext.Response.Headers.Remove("Access-Control-Allow-Headers");
                actionExecutedContext.Response.Headers.Remove("Access-Control-Allow-Methods");
                actionExecutedContext.Response.Headers.Remove("Access-Control-Allow-Credentials");

                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", hostname);
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

                this.hostname = null;
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}