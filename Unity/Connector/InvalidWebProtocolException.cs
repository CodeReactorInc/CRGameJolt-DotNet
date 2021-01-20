using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Exception that represent invalid or a not supported web protocol
    /// </summary>
    /// <seealso cref="WebProtocol"/>
    /// <seealso cref="URLConstructor"/>
    /// <seealso cref="WebCaller"/>
    public class InvalidWebProtocolException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InvalidWebProtocolException"/>
        /// </summary>
        public InvalidWebProtocolException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="InvalidWebProtocolException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public InvalidWebProtocolException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidWebProtocolException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public InvalidWebProtocolException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidWebProtocolException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public InvalidWebProtocolException(string message, Exception inner) : base(message, inner) { }
    }
}
