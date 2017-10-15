namespace WebServer.Server.Http.Contracts
{
    using System.Collections.Generic;

    using Enums;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        IHttpHeaderCollection Headers { get; }

        string Path { get; }

        IDictionary<string, string> UrlParameters { get; }

        RequestMethod Method { get; }

        string Url { get; }

        IDictionary<string, string> QueryParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}