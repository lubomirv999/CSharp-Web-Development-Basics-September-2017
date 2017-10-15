namespace WebServer.Server.Http
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, IHttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, IHttpHeader>();
        }

        public void Add(IHttpHeader header)
        {
            CommonValidator.ThrowIfNull(header, nameof(header));

            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.ThrowIfNull(key, nameof(key));
            return this.headers.ContainsKey(key);
        }

        public IHttpHeader GetHeader(string key)
        {
            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException("Header with given key is not present in the collection");
            }

            return this.headers[key];
        }

        public override string ToString() => string.Join(Environment.NewLine, this.headers);
    }
}