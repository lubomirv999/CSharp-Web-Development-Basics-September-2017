namespace MyMiniWebServer
{
    using MyMiniWebServer.ByTheCakeApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new ByTheCakeApp();
            mainApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(8080, appRouteConfig);
            webServer.Run();
        }
    }
}