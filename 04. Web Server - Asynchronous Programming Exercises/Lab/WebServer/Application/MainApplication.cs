namespace WebServer.Application
{
    using Application.Controllers;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute(
                "/", 
                new GetRequestHandler(httpContext => new HomeController().Index()));

            appRouteConfig.AddRoute(
                "/user/{(?<name>[a-z]+)}",
                new GetRequestHandler(
                    httpContext => 
                        new UserController()
                            .Details(httpContext.UrlParameters["name"])));

            appRouteConfig.AddRoute(
                "/register",
                new PostRequestHandler(
                    httpContext => 
                        new UserController()
                            .RegisterPost(httpContext.FormData["name"])));

            appRouteConfig.AddRoute(
                "/register",
                new GetRequestHandler(
                    httpContext => 
                        new UserController()
                            .RegisterGet()));
        }
    }
}