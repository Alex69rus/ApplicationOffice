using System;

namespace ApplicationOffice.Common.Core.Exceptions
{
    public class NotFoundException : AoException
    {
        public NotFoundException(string message, int errorCode)
            : base(message, errorCode) { }

        public NotFoundException(string message, int errorCode, Exception inner)
            : base(message, errorCode, inner) { }
    }
}