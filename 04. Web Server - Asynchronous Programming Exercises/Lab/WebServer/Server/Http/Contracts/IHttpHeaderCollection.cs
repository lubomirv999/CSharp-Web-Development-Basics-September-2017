namespace WebServer.Server.Http.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(IHttpHeader header);

        bool ContainsKey(string key);

        IHttpHeader GetHeader(string key);
    }
}