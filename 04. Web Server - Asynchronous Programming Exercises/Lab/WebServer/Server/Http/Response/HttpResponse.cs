namespace WebServer.Server.Http.Response
{
    using System.Text;

    using Common;
    using Contracts;
    using Enums;

    public abstract class HttpResponse : IHttpResponse
    {
        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();            
        }

        public IHttpHeaderCollection Headers { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        public virtual string Response
        {
            get
            {
                var response = new StringBuilder();
                var statusCode = (int)this.StatusCode;
                response.AppendLine($"HTTP/1.1 {statusCode} {this.StatusCode}");

                response.AppendLine($"{this.Headers.ToString()}");
                response.AppendLine();

                return response.ToString();
            }
        }

        public void AddHeader(IHttpHeader header)
        {
            CommonValidator.ThrowIfNull(header, nameof(header));

            this.Headers.Add(header);
        }
    }
}