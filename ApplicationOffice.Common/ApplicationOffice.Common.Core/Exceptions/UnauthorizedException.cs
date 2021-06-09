using System;

namespace ApplicationOffice.Common.Core.Exceptions
{
    public class UnauthorizedException : AoException
    {
        public UnauthorizedException(string message, int errorCode) 
            : base(message, errorCode) { }
            
        public UnauthorizedException(string message, int errorCode, Exception inner) 
            : base(message, errorCode, inner) { }
    }
}