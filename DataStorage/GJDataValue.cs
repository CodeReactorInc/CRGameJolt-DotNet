namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A changeable value that can be a Integer or a String
    /// </summary>
    public class GJDataValue
    {
        /// <value>
        /// Are the type of DataStorage value used by converter
        /// </value>
        public GJDataType Type
        {
            get;
            private set;
        }
        /// <value>
        /// A buffer in string format with a unsafe value
        /// </value>
        public string StringBuffer
        {
            get;
            private set;
        }
        /// <value>
        /// A buffer in integer format with a unsafe value
        /// </value>
        public int IntegerBuffer
        {
            get;
            private set;
        }

        /// <summary>
        /// Create a GJDataValue with type null and with buffers cleared
        /// </summary>
        public GJDataValue()
        {
            Type = GJDataType.NULL;
            IntegerBuffer = 0;
            StringBuffer = null;
        }

        /// <summary>
        /// Create a GJDataValue with type string and assign data as value
        /// </summary>
        /// <param name="data">The value to assign in StringBuffer</param>
        public GJDataValue(string value)
        {
            Type = GJDataType.STRING;
            StringBuffer = value;
            IntegerBuffer = 0;
        }

        /// <summary>
        /// Create a GJDataValue with type integer and assign data as value
        /// </summary>
        /// <param name="data">The value to assign in IntegerBuffer</param>
        public GJDataValue(int value)
        {
            Type = GJDataType.INTEGER;
            IntegerBuffer = value;
            StringBuffer = null;
        }

        /// <summary>
        /// Set a new value, change the type and clear IntegerBuffer
        /// </summary>
        /// <param name="value">The new value to assign</param>
        public void Set(string value)
        {
            Type = GJDataType.STRING;
            StringBuffer = value;
            IntegerBuffer = 0;
        }

        /// <summary>
        /// Set a new value, change the type and clear StringBuffer
        /// </summary>
        /// <param name="value">The new value to assign</param>
        public void Set(int value)
        {
            Type = GJDataType.INTEGER;
            IntegerBuffer = value;
            StringBuffer = null;
        }

        /// <summary>
        /// Clear all buffers and change the type for null
        /// </summary>
        public void Clear()
        {
            Type = GJDataType.NULL;
            IntegerBuffer = 0;
            StringBuffer = null;
        }

        /// <summary>
        /// Make add mathematical operation, if GJDataType is a integer
        /// </summary>
        /// <param name="value">Value to add with IntegerBuffer</param>
        public void Add(int value)
        {
            if (Type != GJDataType.INTEGER) throw new InvalidDataTypeException("Can't add value in not integer type");
            IntegerBuffer += value;
        }

        /// <summary>
        /// Make subtract mathematical operation, if GJDataType is a integer
        /// </summary>
        /// <param name="value">Value to subtract with IntegerBuffer</param>
        public void Subtract(int value)
        {
            if (Type != GJDataType.INTEGER) throw new InvalidDataTypeException("Can't subtract value in not integer type");
            IntegerBuffer -= value;
        }

        /// <summary>
        /// Make multiply mathematical operation, if GJDataType is a integer
        /// </summary>
        /// <param name="value">Value to multiply with IntegerBuffer</param>
        public void Multiply(int value)
        {
            if (Type != GJDataType.INTEGER) throw new InvalidDataTypeException("Can't multiply value in not integer type");
            IntegerBuffer *= value;
        }

        /// <summary>
        /// Make divide mathematical operation, if GJDataType is a integer
        /// </summary>
        /// <param name="value">Value to divide with IntegerBuffer</param>
        public void Divide(int value)
        {
            if (Type != GJDataType.INTEGER) throw new InvalidDataTypeException("Can't divide value in not integer type");
            IntegerBuffer /= value;
        }

        /// <summary>
        /// Append a string in StringBuffer, if GJDataType is a string
        /// </summary>
        /// <param name="value">String to append</param>
        public void Append(string value)
        {
            if (Type != GJDataType.STRING) throw new InvalidDataTypeException("Can't append value in not string type");
            StringBuffer += value;
        }

        /// <summary>
        /// Preappend a string in StringBuffer, if GJDataType is a string
        /// </summary>
        /// <param name="value">String to preappend</param>
        public void Prepend(string value)
        {
            if (Type != GJDataType.STRING) throw new InvalidDataTypeException("Can't preappend value in not string type");
            StringBuffer = value + StringBuffer;
        }

        /// <summary>
        /// Implicit get the value in string type
        /// </summary>
        /// <param name="value">Value to convert</param>
        public static implicit operator string(GJDataValue value) {
            if (value.Type != GJDataType.STRING) throw new InvalidDataTypeException("Can't convert GJDataValue to string");
            return value.StringBuffer;
        }

        /// <summary>
        /// Implicit get the value in int type
        /// </summary>
        /// <param name="value">Value to convert</param>
        public static implicit operator int(GJDataValue value)
        {
            if (value.Type != GJDataType.STRING) throw new InvalidDataTypeException("Can't convert GJDataValue to int");
            return value.IntegerBuffer;
        }
    }
}
