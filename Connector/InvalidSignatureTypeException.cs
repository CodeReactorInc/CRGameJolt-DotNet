using System;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Represent a invaliwd or not supported signature type
    /// </summary>
    public class InvalidSignatureTypeException : Exception
    {
        public InvalidSignatureTypeException() : base() { }
        public InvalidSignatureTypeException(string message) : base(message) { }
        public InvalidSignatureTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
