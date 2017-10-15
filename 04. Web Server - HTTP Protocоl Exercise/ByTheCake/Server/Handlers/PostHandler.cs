using System;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.Handlers
{
    public class POSTHandler : RequestHandler
    {
        public POSTHandler(Func<IHttpContext, IHttpResponse> func) : base(func)
        {
        }
    }
}