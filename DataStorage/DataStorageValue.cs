using System;
using System.Text;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A value with a dynamic type that can be a <c>int</c> or a <c>string</c>
    /// </summary>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="UserDataStorage"/>
    public class DataStorageValue
    {

        /// <value>
        /// Are the type of Data Storage value used by converter
        /// </value>
        public DSValueType Type { get; private set; }

        /// <value>
        /// A buffer in <c>string</c> format or null if <see cref="Type"/> isn't <see cref="DSValueType.STRING"/>
        /// </value>
        public string StringBuffer { get; private set; }

        /// <value>
        /// A buffer in <c>int</c> format or null if <see cref="Type"/> isn't <see cref="DSValueType.INTEGER"/>
        /// </value>
        public int? IntegerBuffer { get; private set; }

        /// <summary>
        /// Create a <see cref="DataStorageValue"/> with <see cref="DSValueType.STRING"/> and assign <paramref name="value"/> as data
        /// </summary>
        /// <param name="data">The value to assign in <see cref="StringBuffer"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public DataStorageValue(string value)
        {
            if (Encoding.UTF8.GetBytes(value).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            Type = DSValueType.STRING;
            StringBuffer = value;
            IntegerBuffer = null;
        }

        /// <summary>
        /// Create a <see cref="DataStorageValue"/> with <see cref="DSValueType.INTEGER"/> and assign <paramref name="value"/> as data
        /// </summary>
        /// <param name="data">The value to assign in <see cref="IntegerBuffer"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public DataStorageValue(int value)
        {
            if (Encoding.UTF8.GetBytes(value.ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            Type = DSValueType.INTEGER;
            IntegerBuffer = value;
            StringBuffer = null;
        }

        /// <summary>
        /// Set a new value, change the <see cref="Type"/> to <see cref="DSValueType.STRING"/> and clear <see cref="IntegerBuffer"/>
        /// </summary>
        /// <param name="value">The new value to assign</param>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Set(string value)
        {
            if (Encoding.UTF8.GetBytes(value).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            Type = DSValueType.STRING;
            StringBuffer = value;
            IntegerBuffer = null;
        }

        /// <summary>
        /// Set a new value, change the <see cref="Type"/> to <see cref="DSValueType.INTEGER"/> and clear <see cref="StringBuffer"/>
        /// </summary>
        /// <param name="value">The new value to assign</param>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Set(int value)
        {
            if (Encoding.UTF8.GetBytes(value.ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            Type = DSValueType.INTEGER;
            IntegerBuffer = value;
            StringBuffer = null;
        }

        /// <summary>
        /// Execute add mathematical operation, if <see cref="Type"/> is a <see cref="DSValueType.INTEGER"/>
        /// </summary>
        /// <param name="value">Value to add with <see cref="IntegerBuffer"/></param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.INTEGER"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Add(int value)
        {
            if (Type != DSValueType.INTEGER) throw new InvalidDataTypeException("Can't execute add because the type isn't a integer");
            if (Encoding.UTF8.GetBytes((IntegerBuffer + value).ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            IntegerBuffer += value;
        }

        /// <summary>
        /// Execute subtract mathematical operation, if <see cref="Type"/> is a <see cref="DSValueType.INTEGER"/>
        /// </summary>
        /// <param name="value">Value to subtract with <see cref="IntegerBuffer"/></param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.INTEGER"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Subtract(int value)
        {
            if (Type != DSValueType.INTEGER) throw new InvalidDataTypeException("Can't execute subtract because the type isn't a integer");
            if (Encoding.UTF8.GetBytes((IntegerBuffer - value).ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            IntegerBuffer -= value;
        }

        /// <summary>
        /// Execute multiply mathematical operation, if <see cref="Type"/> is a <see cref="DSValueType.INTEGER"/>
        /// </summary>
        /// <param name="value">Value to multiply with <see cref="IntegerBuffer"/></param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.INTEGER"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Multiply(int value)
        {
            if (Type != DSValueType.INTEGER) throw new InvalidDataTypeException("Can't execute multiply because the type isn't a integer");
            if (Encoding.UTF8.GetBytes((IntegerBuffer * value).ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            IntegerBuffer *= value;
        }

        /// <summary>
        /// Execute divide mathematical operation, if <see cref="Type"/> is a <see cref="DSValueType.INTEGER"/>
        /// </summary>
        /// <param name="value">Value to divide with <see cref="IntegerBuffer"/></param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.INTEGER"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        public void Divide(int value)
        {
            if (Type != DSValueType.INTEGER) throw new InvalidDataTypeException("Can't execute divide because the type isn't a integer");
            if (Encoding.UTF8.GetBytes((IntegerBuffer / value).ToString()).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            IntegerBuffer /= value;
        }

        /// <summary>
        /// Append a <c>string</c> in <see cref="StringBuffer"/>, if <see cref="Type"/> is a <see cref="DSValueType.STRING"/>
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.STRING"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        /// <param name="value">String to be appended</param>
        public void Append(string value)
        {
            if (Type != DSValueType.STRING) throw new InvalidDataTypeException("Can't append string in StringBuffer, type isn't string");
            if (Encoding.UTF8.GetBytes(StringBuffer + value).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            StringBuffer += value;
        }

        /// <summary>
        /// Prepend a <c>string</c> in <see cref="StringBuffer"/>, if <see cref="Type"/> is a <see cref="DSValueType.STRING"/>
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.STRING"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if value are bigger than 16MB</exception>
        /// <param name="value">String to be prepended</param>
        public void Prepend(string value)
        {
            if (Type != DSValueType.STRING) throw new InvalidDataTypeException("Can't prepend string in StringBuffer, type isn't string");
            if (Encoding.UTF8.GetBytes(value + StringBuffer).Length > 16000000) throw new ArgumentOutOfRangeException("Value are bigger than 16MB");
            StringBuffer = value + StringBuffer;
        }

        /// <summary>
        /// Implicit get of value from <see cref="StringBuffer"/>
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.STRING"/></exception>
        /// <param name="value">Value to convert from</param>
        public static implicit operator string(DataStorageValue value)
        {
            if (value.Type != DSValueType.STRING) throw new InvalidDataTypeException("Can't convert DataStorageValue to string");
            return value.StringBuffer;
        }

        /// <summary>
        /// Implicit get of value from <see cref="IntegerBuffer"/>
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Type"/> isn't a <see cref="DSValueType.INTEGER"/></exception>
        /// <param name="value">Value to convert from</param>
        public static implicit operator int(DataStorageValue value)
        {
            if (value.Type != DSValueType.STRING) throw new InvalidDataTypeException("Can't convert DataStorageValue to int");
            return (int)value.IntegerBuffer;
        }
    }
}
