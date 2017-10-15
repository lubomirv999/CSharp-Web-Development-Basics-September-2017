namespace WebServer.Server.Http
{
    using Common;
    using Contracts;

    public class HttpHeader : IHttpHeader
    {
        public HttpHeader(string key, string value)
        {
            CommonValidator.ThrowIfNull(key, nameof(key));
            CommonValidator.ThrowIfNull(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public override string ToString() => $"{this.Key}:{this.Value}";
    }
}