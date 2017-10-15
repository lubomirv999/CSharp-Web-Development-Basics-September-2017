namespace WebServer
{
    using Server;
    using Server.Routing;
    using WebServer.Application;

    public class Startup
    {
        public static void Main()
        {
            // To run it type 127.0.0.1:1337
            var app = new MainApplication();
            var routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            var webServer = new WebServer(1337, routeConfig);
            webServer.Run();
        }
    }
}