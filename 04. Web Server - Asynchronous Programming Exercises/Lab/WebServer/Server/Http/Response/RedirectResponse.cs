namespace WebServer.Server.Http.Response
{
    using Enums;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
        {
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader(new HttpHeader("Location", redirectUrl));
        }
    }
}