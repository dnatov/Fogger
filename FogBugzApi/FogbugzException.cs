using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzApi
{
    /// <summary>
    /// Custom exception for throwing returned API error codes.
    /// </summary>
    public class FogbugzException : Exception
    {
        public FogbugzException (int errorCode, string errorString) : base(errorString)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode;
    }
}

