﻿namespace WebServer.Server
{
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Contracts;
    using Routing;
    using Routing.Contracts;

    public class WebServer : IRunnable
    {
        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener tcpListener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();

            this.isRunning = true;

            Task
                .Run(async () => 
                {
                    await this.ListenLoop();
                })
                .GetAwaiter()
                .GetResult();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                try
                {
                    var client = await this.tcpListener.AcceptSocketAsync();
                    var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                    var connection = connectionHandler.ProcessRequetAsync();
                    connection.GetAwaiter().GetResult();
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
        }
    }
}