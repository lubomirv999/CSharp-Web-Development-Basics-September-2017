namespace WebServer.Server.Handlers
{
    using System;

    using Contracts;
    using Http;
    using Http.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> getResponseByRequest;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> getResponseByRequest)
        {
            this.getResponseByRequest = getResponseByRequest;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var httpResponse = this.getResponseByRequest(httpContext.Request);

            httpResponse.AddHeader(new HttpHeader("Content-Type", "text/html"));

            return httpResponse;
        }
    }
}