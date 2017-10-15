namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Enums;
    using Handlers.Contracts;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<RequestMethod, IDictionary<string, IRequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<RequestMethod, IDictionary<string, IRequestHandler>>();

            foreach (var requestMethod in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.routes.Add(requestMethod, new Dictionary<string, IRequestHandler>());
            }
        }

        public IDictionary<RequestMethod, IDictionary<string, IRequestHandler>> Routes => this.routes;

        public void AddRoute(string route, IRequestHandler requestHandler)
        {
            var requestHandlerTypeName = requestHandler.GetType().ToString().ToLower();

            if (requestHandlerTypeName.Contains("get"))
            {
                this.routes[RequestMethod.Get].Add(route, requestHandler);
            }
            else if (requestHandlerTypeName.Contains("post"))
            {
                this.routes[RequestMethod.Post].Add(route, requestHandler);
            }
        }
    }
}