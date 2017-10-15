﻿namespace WebServer.Server.Handlers.Contracts
{
    using Http.Contracts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}