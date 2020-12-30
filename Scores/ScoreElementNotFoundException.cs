using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.Scores
{
    /// <summary>
    /// If a XML element necessary for <see cref="ScoreValue"/> initialization isn't found, this is throwed
    /// </summary>
    /// <seealso cref="ScoreValue"/>
    /// <seealso cref="ScoreTable"/>
    /// <seealso cref="TableManager"/>
    public class ScoreElementNotFoundException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="ScoreElementNotFoundException"/>
        /// </summary>
        public ScoreElementNotFoundException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="ScoreElementNotFoundException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public ScoreElementNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="ScoreElementNotFoundException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public ScoreElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="ScoreElementNotFoundException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public ScoreElementNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
