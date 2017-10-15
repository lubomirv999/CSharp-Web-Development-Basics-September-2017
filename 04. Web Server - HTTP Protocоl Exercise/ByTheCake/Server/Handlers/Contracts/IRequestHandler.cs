using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.Handlers.Contracts
{
    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}