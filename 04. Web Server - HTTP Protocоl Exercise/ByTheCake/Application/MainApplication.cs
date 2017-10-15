using WebServer.Application.Controllers;
using WebServer.Server.Contracts;
using WebServer.Server.Handlers;
using WebServer.Server.Routing.Contracts;

namespace WebServer.Application
{
    public class MainApplication : IApplication
    {
	    public void Start(IAppRouteConfig appRouteConfig)
	    {
		    appRouteConfig.AddRoute("/", new GETHandler(httpContext => new HomeController().Index()));

			appRouteConfig.AddRoute("/about", new GETHandler(httpContext => new HomeController().AboutUs()));

			appRouteConfig.AddRoute("/add", new GETHandler(httpContext => new CakeController().AddCakeGet()));

		    appRouteConfig.AddRoute("/add", new POSTHandler(httpContext => new CakeController().AddCakePost(httpContext.Request.FormData["name"],httpContext.Request.FormData["price"] )));

		    appRouteConfig.AddRoute("/search",
			    new GETHandler(httpContext => new CakeController().SearchGet(httpContext.Request.QueryParameters)));

		    appRouteConfig.AddRoute("/calculator",
			    new GETHandler(httpContext => new HomeController().CalculatorGet()));

		    appRouteConfig.AddRoute("/calculator",
			    new POSTHandler(httpContext => new HomeController().CalculatorPost(httpContext.Request.FormData["firstNumber"], httpContext.Request.FormData["secondNumber"], httpContext.Request.FormData["sign"])));

		    appRouteConfig.AddRoute("/login", new GETHandler(httpContext => new HomeController().LoginGet()));

		    appRouteConfig.AddRoute("/login", new POSTHandler(httpContext => new HomeController().LoginPost(httpContext.Request.FormData["username"], httpContext.Request.FormData["pass"])));
		}
    }
}