using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebServer.Application.Views;
using WebServer.Server.Enums;
using WebServer.Server.Handlers.Contracts;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;
using WebServer.Server.Routing.Contracts;

namespace Webserver.Server.Handlers
{
    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            foreach (KeyValuePair<string, IRoutingContext> kvp in this.serverRouteConfig.Routes[httpContext.Request.RequestMethod])
            {
                string pattern = kvp.Key;
                Regex regex = new Regex(pattern);
                Match match = regex.Match(httpContext.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (string parameter in kvp.Value.Parameters)
                {
                    httpContext.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return new ViewResponse(HttpStatusCode.NotFound, new NotFoundView());
        }
    }
}