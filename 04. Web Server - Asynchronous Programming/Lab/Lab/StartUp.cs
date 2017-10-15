namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            // Task 1 Even Numbers Thread
            // EvenNumbersThread();

            // Task 2 Slice File
            // SliceFile();

            // Task 3 Simple Web Server
            // SimpleWebServer();
        }        

        // Task 1
        private static void EvenNumbersThread()
        {
            var numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var start = int.Parse(numbers[0]);
            var end = int.Parse(numbers[1]);

            Thread events = new Thread(() => PrintEvenNumbers(start, end));
            events.Start();
            events.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            var result = new List<int>();

            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
        // Task 1

        // Task 2
        private static void SliceFile()
        {
            var path = Console.ReadLine();
            var destination = Console.ReadLine();
            var pieces = int.Parse(Console.ReadLine());

            SliceAsync(path, destination, pieces);

            Console.WriteLine("Anything else?");
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                var fileInfo = new FileInfo(sourceFile);
                var partLength = (source.Length / parts) + 1;
                var currentByte = 0;

                for (int currentPart = 1; currentPart <= parts; currentPart++)
                {
                    var filePath = string.Format("{0}/Part-{1}{2}", destinationPath, currentPart, fileInfo.Extension);

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        var buffer = new byte[4096];

                        while (currentByte <= partLength * currentPart)
                        {
                            var readBytesCount = source.Read(buffer, 0, buffer.Length);

                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }
                }
            }

            Console.WriteLine("Slice complete.");
        }

        private static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }
        // Task 2

        // Task 3
        private static void SimpleWebServer()
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 1300;
            var listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1: {port}");

            var task = Task.Run(() => ConnectWithTcpClient(listener));
            task.Wait();
        }

        public static async Task ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                var buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);

                var message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(message);

                var data = Encoding.ASCII.GetBytes("Hello from server!");
                client.GetStream().Write(data, 0, data.Length);

                Console.WriteLine("Closing connection.");
                client.GetStream().Dispose();
            }
        }
        // Task 3
    }
}