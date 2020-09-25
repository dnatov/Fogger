using System;
using System.Collections.Generic;
using System.Text;

namespace Fogger.Exceptions
{
    /// <summary>
    /// Custom exception for throwing returned API error codes.
    /// </summary>
    public class FoggerException : Exception
    {
        public FoggerException(int errorCode, string errorString) : base(errorString)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode;
    }
}

