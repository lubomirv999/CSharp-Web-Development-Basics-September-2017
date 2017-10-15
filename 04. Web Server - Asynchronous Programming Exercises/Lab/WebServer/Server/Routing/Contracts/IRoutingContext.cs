namespace WebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Handlers.Contracts;

    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        IRequestHandler RequestHandler { get; }
    }
}