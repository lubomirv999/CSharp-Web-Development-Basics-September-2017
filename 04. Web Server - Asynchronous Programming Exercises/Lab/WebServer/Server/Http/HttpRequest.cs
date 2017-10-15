namespace WebServer.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contracts;
    using Enums;
    using Exceptions;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; }

        public IHttpHeaderCollection Headers { get; }

        public string Path { get; private set; }

        public IDictionary<string, string> UrlParameters { get; }

        public RequestMethod Method { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> QueryParameters { get; }

        public void AddUrlParameter(string key, string value)
        {
            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            var requestLines = requestString
                .Split(Environment.NewLine);

            var requestLine = requestLines
                .FirstOrDefault()
                ?.Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.Method = this.ParseRequestMethod(requestLines[0]);
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }

        private void ParseFormData(string formDataQuery)
        {
            if (this.Method != RequestMethod.Post)
            {
                return;
            }

            this.ParseQuery(formDataQuery, this.FormData);
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

            var query = this.Url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[1];
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryPair in queryPairs)
            {
                var queryPairArgs = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (queryPairArgs.Length != 2)
                {
                    continue;
                }

                var key = WebUtility.UrlDecode(queryPairArgs[0]);
                var value = WebUtility.UrlDecode(queryPairArgs[1]);

                dict.Add(key, value);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            var indexOfLastHeader = Array.IndexOf(requestLines, string.Empty);
            for (var i = 1; i < indexOfLastHeader; i++)
            {
                var headerArgs = requestLines[i]
                    .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                var header = new HttpHeader(headerArgs[0], headerArgs[1]);
                this.Headers.Add(header);
            }

            if (!this.Headers.ContainsKey("Host"))
            {
                throw new InvalidOperationException("No host header was provided");
            }
        }

        private RequestMethod ParseRequestMethod(string requestLine)
        {
            var method = requestLine.Split(' ')[0];

            if (!Enum.TryParse(method, true, out RequestMethod parsedMethod))
            {
                throw new InvalidOperationException("Invalid request method");
            }

            return parsedMethod;
        }
    }
}