﻿namespace WebServer.Server.Handlers
{
    using System;

    using Http.Contracts;

    public class PostRequestHandler : RequestHandler
    {
        public PostRequestHandler(Func<IHttpRequest, IHttpResponse> getResponseByRequest) 
            : base(getResponseByRequest)
        {
        }
    }
}