using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzApi
{
    public static class HttpClientFactory
    {
        public static IHttpClient CreateHttpClient()
        {
            //TODO: Put version dependent HttpClient code here
            return new HttpClientHelper();
        }
    }
}
