namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class StartUp
    {
        static void Main()
        {
            // Task 1 Url Decode
            // UrlDecode();

            // Task 2 Validate Url
            // ValidateUrl();

            // Task 3 Request Parser
            // RequestParser();
        }

        private static void UrlDecode()
        {
            string url = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(url);
            Console.WriteLine(decodedUrl);
        }

        private static void ValidateUrl()
        {
            var url = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(url);
            var parsedUrl = new Uri(decodedUrl);

            if (string.IsNullOrEmpty(parsedUrl.Scheme))
            {
                Console.WriteLine("Invalid URL");
            }

            if (string.IsNullOrEmpty(parsedUrl.Host))
            {
                Console.WriteLine("Invalid URL");
            }

            if (string.IsNullOrEmpty(parsedUrl.AbsolutePath))
            {
                Console.WriteLine("Invalid URL");
            }

            Console.WriteLine($"Protocol: {parsedUrl.Scheme}");
            Console.WriteLine($"Host: {parsedUrl.Host}");
            Console.WriteLine($"Port: {parsedUrl.Port}");
            Console.WriteLine($"Query: {parsedUrl.AbsolutePath}");
            Console.WriteLine($"Fragment: {parsedUrl.Fragment}");
        }

        private static void RequestParser()
        {
            // URL - collection of valid methods
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var urlParts = line.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var path = $"/{urlParts[0]}";
                var method = urlParts[1];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add(method);
            }

            var request = Console.ReadLine();
            var requestParts = request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var requestMethod = requestParts[0];
            var requestUrl = requestParts[1];
            var requestProtocol = requestParts[2];

            var responseStatus = 404;
            var responseStatusText = "Not Found";

            if (validUrls.ContainsKey(requestUrl) && validUrls[requestUrl].Contains(requestMethod.ToLower()))
            {
                responseStatus = 200;
                responseStatusText = "OK";
            }

            Console.WriteLine($"{requestProtocol} {responseStatus} {responseStatusText}");
            Console.WriteLine($"Content-Length: {responseStatusText.Length}");
            Console.WriteLine("Content-type: text/plain");
            Console.WriteLine();
            Console.WriteLine(responseStatusText);
        }
    }
}