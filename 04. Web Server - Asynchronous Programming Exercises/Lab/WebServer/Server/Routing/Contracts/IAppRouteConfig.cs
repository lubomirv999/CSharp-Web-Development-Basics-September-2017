namespace WebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Handlers.Contracts;

    public interface IAppRouteConfig
    {
        IDictionary<RequestMethod, IDictionary<string, IRequestHandler>> Routes { get; }

        void AddRoute(string route, IRequestHandler requestHandler);
    }
}