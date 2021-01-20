using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Users.Trophies
{
    /// <summary>
    /// If a XML element necessary for <see cref="Trophy"/> initialization isn't found, this is throwed
    /// </summary>
    /// <seealso cref="Trophy"/>
    /// <seealso cref="TrophiesManager"/>
    /// <seealso cref="TrophyDifficulty"/>
    public class TrophyElementNotFoundException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="TrophyElementNotFoundException"/>
        /// </summary>
        public TrophyElementNotFoundException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="TrophyElementNotFoundException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public TrophyElementNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="TrophyElementNotFoundException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public TrophyElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="TrophyElementNotFoundException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public TrophyElementNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
