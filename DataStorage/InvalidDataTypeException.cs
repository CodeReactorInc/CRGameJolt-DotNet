using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// This represent a invalid type conversion throught DataStorage
    /// </summary>
    public class InvalidDataTypeException : Exception
    {
        public InvalidDataTypeException() : base() { }
        public InvalidDataTypeException(string message) : base(message) { }
        public InvalidDataTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
