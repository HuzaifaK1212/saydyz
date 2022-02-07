using System;

namespace Platform.Utilities.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string msg) : base(msg)
        {}
    }
}
