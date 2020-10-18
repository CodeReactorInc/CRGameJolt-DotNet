using System;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Represent a not supported or invalid web protocol
    /// </summary>
    public class InvalidWebProtocolException : Exception
    {
        public InvalidWebProtocolException() : base() { }
        public InvalidWebProtocolException(string message) : base(message) { }
        public InvalidWebProtocolException(string message, Exception inner) : base(message, inner) { }
    }
}
