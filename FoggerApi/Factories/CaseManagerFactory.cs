using Fogger.Interfaces;
using Fogger.Services;

namespace Fogger.Factories
{
    internal static class CaseManagerFactory
    {
        internal static ICaseManager CreateCaseManager(int version, int minVersion, IHttpClient httpClient)
        {
            //TODO: Add version dependent logic for different FogBugz Versions
            return new CaseManagerV8(httpClient);
        }
    }
}
