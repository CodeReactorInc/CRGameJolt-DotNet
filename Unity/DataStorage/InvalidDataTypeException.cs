using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Exception that represent invalid conversion in <see cref="DataStorageValue"/>
    /// </summary>
    /// <seealso cref="DataStorageValue"/>
    /// <seealso cref="DSValueType"/>
    public class InvalidDataTypeException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InvalidDataTypeException"/>
        /// </summary>
        public InvalidDataTypeException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="InvalidDataTypeException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public InvalidDataTypeException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidDataTypeException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public InvalidDataTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="InvalidDataTypeException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public InvalidDataTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
