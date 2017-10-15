using System;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.Handlers
{
    public class GETHandler : RequestHandler
    {
        public GETHandler(Func<IHttpContext, IHttpResponse> func) : base(func)
        {
        }
    }
}