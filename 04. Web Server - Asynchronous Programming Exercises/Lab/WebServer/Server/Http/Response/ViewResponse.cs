namespace WebServer.Server.Http.Response
{
    using System;

    using Contracts;
    using Enums;

    public class ViewResponse : HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode, IView view)
        {
            this.ValidateStatusCode(statusCode);

            this.StatusCode = statusCode;
            this.view = view;
        }

        public override string Response => $"{base.Response}{this.view.View()}";

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if (statusCodeNumber > 299 && statusCodeNumber < 400)
            {
                throw new ArgumentException("Invalid status code");
            }
        }
    }
}