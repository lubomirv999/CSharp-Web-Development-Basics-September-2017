namespace WebServer.Server
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using Handlers;
    using Http;
    using Routing.Contracts;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequetAsync()
        {
            var request = await this.ReadRequest();

            var httpContext = new HttpContext(request);

            var response = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

            var toBytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.Response));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            Console.WriteLine(request);
            Console.WriteLine(response.Response);

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            var request = string.Empty;
            var data = new ArraySegment<byte>(new byte[1024]);

            var numberOfBytesRead = 0;

            while ((numberOfBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request += Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            return request;
        }
    }
}