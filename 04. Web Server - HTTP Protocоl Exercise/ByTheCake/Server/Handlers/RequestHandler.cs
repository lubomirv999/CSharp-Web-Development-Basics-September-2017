using System;
using WebServer.Server.Handlers.Contracts;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.Handlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpContext, IHttpResponse> func;

        protected RequestHandler(Func<IHttpContext, IHttpResponse> func)
        {
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func.Invoke(httpContext);

            httpResponse.AddHeader("Content-Type", "text/html");

            return httpResponse;
        }
    }
}