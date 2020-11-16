using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Exception that represent a invalid or not supported session status
    /// </summary>
    /// <seealso cref="SessionStatus"/>
    /// <seealso cref="SessionManager"/>
    public class InvalidSessionStatusException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InvalidSessionStatusException"/>
        /// </summary>
        public InvalidSessionStatusException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="InvalidSessionStatusException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public InvalidSessionStatusException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidSessionStatusException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public InvalidSessionStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidSessionStatusException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public InvalidSessionStatusException(string message, Exception inner) : base(message, inner) { }
    }
}
