using FogBugzApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestApi
{
    //Test project integrating API
    class Program
    {
        static void Main(string[] args)
        {
            //Semi colon seperated fogbugz enviroment variable in the form of domain;user
            var SuperSecretLogin = Environment.GetEnvironmentVariable("Fogbugz").Split(';');
            var Domain = SuperSecretLogin[0];
            var UserName = SuperSecretLogin[1];
            Console.Write("Enter Password:"); var Password = Console.ReadLine();

            var Api = new FogBugzApiWrapper(Domain, UserName, Password);
            var filters = Api.GetFilters();

            //Always logoff to invalidate token
            Api.Logoff();
        }

    }
}
