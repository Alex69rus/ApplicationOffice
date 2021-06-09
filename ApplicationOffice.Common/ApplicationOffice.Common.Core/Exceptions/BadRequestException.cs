using System;

namespace ApplicationOffice.Common.Core.Exceptions
{
    public class BadRequestException : AoException
    {
        public BadRequestException(string message, int errorCode)
            : base(message, errorCode) { }

        public BadRequestException(string message, int errorCode, Exception inner)
            : base(message, errorCode, inner) { }
    }
}