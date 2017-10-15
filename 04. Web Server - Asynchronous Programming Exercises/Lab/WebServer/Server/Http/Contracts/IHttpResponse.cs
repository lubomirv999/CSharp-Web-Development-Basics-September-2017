namespace WebServer.Server.Http.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        string Response { get; }

        IHttpHeaderCollection Headers { get; }

        HttpStatusCode StatusCode { get; }

        void AddHeader(IHttpHeader header);
    }
}