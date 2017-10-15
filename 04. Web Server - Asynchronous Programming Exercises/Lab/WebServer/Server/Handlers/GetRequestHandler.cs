namespace WebServer.Server.Handlers
{
    using System;

    using Http.Contracts;

    public class GetRequestHandler : RequestHandler
    {
        public GetRequestHandler(Func<IHttpRequest, IHttpResponse> getResponseByRequest) 
            : base(getResponseByRequest)
        {
        }
    }
}