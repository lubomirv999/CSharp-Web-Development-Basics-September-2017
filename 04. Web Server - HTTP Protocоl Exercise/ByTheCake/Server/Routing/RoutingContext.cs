using System.Collections.Generic;
using WebServer.Server.Handlers;
using WebServer.Server.Routing.Contracts;

namespace WebServer.Server.Routing
{
    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler requestHandler, IEnumerable<string> args)
        {
            this.Parameters = args;
            this.RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; set; }
    }
}