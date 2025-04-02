using Fogger.Interfaces;
using Fogger.Services;

namespace Fogger.Factories
{
    public static class HttpClientFactory
    {
        public static IHttpClient CreateHttpClient()
        {
            //TODO: Put version dependent HttpClient code here
            return new HttpClientHelperV8();
        }
    }
}
