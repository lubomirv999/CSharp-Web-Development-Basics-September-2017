namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private const string ParamsRegexPattern = "<\\w+>";

        private readonly Regex paramsRegex;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.Routes = new Dictionary<RequestMethod, IDictionary<string, IRoutingContext>>();
            this.paramsRegex = new Regex(ParamsRegexPattern);

            foreach (var requestMethod in Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>())
            {
                this.Routes.Add(requestMethod, new Dictionary<string, IRoutingContext>());
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var kvp in appRouteConfig.Routes)
            {
                foreach (var requestHandler in kvp.Value)
                {
                    var args = new List<string>();

                    var parsedRegex = this.ParseRoute(requestHandler.Key, args);

                    IRoutingContext routingContext = new RoutingContext(requestHandler.Value, args);

                    this.Routes[kvp.Key].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, List<string> args)
        {
            var parsedRegex = new StringBuilder();
            parsedRegex.Append("^");

            if (requestHandlerKey == "/")
            {
                parsedRegex.Append($"{requestHandlerKey}$");
                return parsedRegex.ToString();
            }

            var tokens = requestHandlerKey.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(args, tokens, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(List<string> args, string[] tokens, StringBuilder parsedRegex)
        {
            for (var i = 0; i < tokens.Length; i++)
            {
                var end = i == tokens.Length - 1 ? "$" : "/";
                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                var match = this.paramsRegex.Match(tokens[i]);

                if (!match.Success)
                {
                    continue;
                }

                var paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parsedRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}