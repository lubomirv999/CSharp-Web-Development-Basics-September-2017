namespace WebServer.Server.Routing
{
    using System.Collections.Generic;
    using Contracts;
    using Handlers.Contracts;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IRequestHandler requestHandler, IEnumerable<string> parameters)
        {
            this.RequestHandler = requestHandler;
            this.Parameters = new List<string>(parameters);
        }

        public IEnumerable<string> Parameters { get; }

        public IRequestHandler RequestHandler { get; }
    }
}