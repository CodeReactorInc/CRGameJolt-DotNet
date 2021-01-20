using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt
{
    /// <summary>
    /// Contains a error from GameJolt Game API and are shared throght all classes that process GameJolt Game API responses
    /// </summary>
    public class GameJoltAPIException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="GameJoltAPIException"/>
        /// </summary>
        public GameJoltAPIException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="GameJoltAPIException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public GameJoltAPIException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="GameJoltAPIException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public GameJoltAPIException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="GameJoltAPIException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public GameJoltAPIException(string message, Exception inner) : base(message, inner) { }
    }
}
