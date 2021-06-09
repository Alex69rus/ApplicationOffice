using System;

namespace ApplicationOffice.Common.Core.Exceptions
{
    public class AoException : Exception
    {
        public readonly int ErrorCode;

        public AoException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public AoException(string message, int errorCode, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}