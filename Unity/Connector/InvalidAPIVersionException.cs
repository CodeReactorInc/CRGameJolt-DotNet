using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Exception that represent a invalid or a not supported GameJolt Game API version
    /// </summary>
    /// <seealso cref="APIVersion"/>
    /// <seealso cref="URLConstructor"/>
    public class InvalidAPIVersionException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InvalidAPIVersionException"/>
        /// </summary>
        public InvalidAPIVersionException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="InvalidAPIVersionException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public InvalidAPIVersionException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidAPIVersionException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public InvalidAPIVersionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidAPIVersionException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public InvalidAPIVersionException(string message, Exception inner) : base(message, inner) { }
    }
}
