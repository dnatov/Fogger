using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzApi
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
