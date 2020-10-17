using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Represent a invalid or a not supported Game API version
    /// </summary>
    public class InvalidAPIVersionException : Exception
    {
        public InvalidAPIVersionException() : base() { }
        public InvalidAPIVersionException(string message) : base(message) { }
        public InvalidAPIVersionException(string message, Exception inner) : base(message, inner) { }
    }
}
