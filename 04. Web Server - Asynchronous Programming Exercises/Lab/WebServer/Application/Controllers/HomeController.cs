namespace WebServer.Application.Controllers
{
    using Application.Views;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HomeIndexView());
        }
    }
}