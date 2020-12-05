using System;
using System.Runtime.Serialization;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Exception that represent that <see cref="DataStorageValue"/> aren't have a <see cref="IGJDataStorage"/> linked
    /// </summary>
    /// <seealso cref="DataStorageValue"/>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="UserDataStorage"/>
    public class NoDataStorageException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="NoDataStorageException"/>
        /// </summary>
        public NoDataStorageException() : base() { }

        /// <summary>
        /// Initialize a new <see cref="NoDataStorageException"/> with a error message
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        public NoDataStorageException(string message) : base(message) { }

        /// <summary>
        /// Initialize a new <see cref="NoDataStorageException"/> with serialized data
        /// </summary>
        /// <param name="info">The <c>SerializationInfo</c> that keed the serialized data about the exception</param>
        /// <param name="context">The <c>StreamingContext</c> that keep context info about destiny and source</param>
        public NoDataStorageException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initialize a new <see cref="NoDataStorageException"/> with a error message and a reference to a internal exception
        /// </summary>
        /// <param name="message">A message that contain the error</param>
        /// <param name="inner">A reference to internal exception or a null reference</param>
        public NoDataStorageException(string message, Exception inner) : base(message, inner) { }
    }
}
