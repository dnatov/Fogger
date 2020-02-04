using FogBugzApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            FogBugzApiWrapper.InitializeApi(@"https://example.fogbugz.com");
            Console.WriteLine($"Version: {FogBugzApiWrapper.Version}");
            Console.WriteLine($"MinVersion: {FogBugzApiWrapper.MinVersion}");
            Console.WriteLine($"Url: {FogBugzApiWrapper.Url}");
        }

    }
}
