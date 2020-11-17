using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Users.Trophies
{
    /// <summary>
    /// Exception that represent a invalid or a not supported <see cref="TrophyDifficulty"/>
    /// </summary>
    /// <seealso cref="APIVersion"/>
    /// <seealso cref="URLConstructor"/>
    public class InvalidTrophyDifficultyException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InvalidTrophyDifficultyException"/>
        /// </summary>
        public InvalidTrophyDifficultyException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="InvalidTrophDifficultyException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public InvalidTrophyDifficultyException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidTrophyDifficultyException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public InvalidTrophyDifficultyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidTrophyDifficultyException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public InvalidTrophyDifficultyException(string message, Exception inner) : base(message, inner) { }
    }
}
