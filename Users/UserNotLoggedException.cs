using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Exception throwed if user aren't logged
    /// </summary>
    /// <seealso cref="GameJolt"/>
    /// <seealso cref="GameJoltMe"/>
    public class UserNotLoggedException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="UserNotLoggedException"/>
        /// </summary>
        public UserNotLoggedException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="UserNotLoggedException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public UserNotLoggedException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="UserNotLoggedException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public UserNotLoggedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="UserNotLoggedException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public UserNotLoggedException(string message, Exception inner) : base(message, inner) { }
    }
}
