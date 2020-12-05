using CodeReactor.CRGameJolt.Connector;
using System;
using System.Text;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A controlled value that has a dynamic type that can be <c>string</c> or <c>int</c>
    /// </summary>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="UserDataStorage"/>
    public class DataStorageValue
    {
        /// <value>
        /// The key associated with this <see cref="DataStorageValue"/> and <see cref="DataStorage"/>
        /// </value>
        public string Key { get; private set; }

        /// <value>
        /// The Data Storaged associated with this <see cref="DataStorageValue"/>
        /// </value>
        public IGJDataStorage DataStorage { get; private set; }

        /// <value>
        /// The buffer that can be a <c>string</c> or a <c>int</c>
        /// </value>
        public object Buffer { get; private set; }

        /// <value>
        /// Speficy the type of buffer to cast
        /// </value>
        public DSValueType Type { get; private set; }

        /// <summary>
        /// Test if <paramref name="cache"/> is a <c>int</c> or <c>string</c> and generate a <see cref="DataStorageValue"/>
        /// </summary>
        /// <param name="cache">Raw data used to create a <see cref="DataStorageValue"/></param>
        /// <param name="key">Key to be assin to <see cref="DataStorageValue"/></param>
        /// <param name="dataStorage">Data Storage to be assign to <see cref="DataStorageValue"/></param>
        /// <returns><paramref name="cache"/> parsed in a <see cref="DataStorageValue"/></returns>
        public static DataStorageValue Parse(string cache, string key, IGJDataStorage dataStorage)
        {
            int icache;
            if (int.TryParse(cache, out icache))
            {
                return new DataStorageValue(icache, key, dataStorage);
            }
            else return new DataStorageValue(cache, key, dataStorage);
        }

        /// <summary>
        /// Create a buffer with <see cref="DSValueType.STRING"/> type
        /// </summary>
        /// <param name="cache">Value to be assign to <see cref="Buffer"/></param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="cache"/> has more than 16MB, this is throwed</exception>
        public DataStorageValue(string cache) : this(cache, null, null) { }

        /// <summary>
        /// Create a buffer with <see cref="DSValueType.INTEGER"/> type
        /// </summary>
        /// <param name="cache">Value to be assign to <see cref="Buffer"/></param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="cache"/> has more than 16MB, this is throwed</exception>
        public DataStorageValue(int cache) : this(cache, null, null) { }

        /// <summary>
        /// Create a buffer with <see cref="DSValueType.STRING"/> type
        /// </summary>
        /// <param name="cache">Value to be assign to <see cref="Buffer"/></param>
        /// <param name="key">Key in the Data Storage that has this value</param>
        /// <param name="dataStorage">Data Storage used to make simple operations</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="cache"/> has more than 16MB, this is throwed</exception>
        public DataStorageValue(string cache, string key, IGJDataStorage dataStorage)
        {
            if (Encoding.UTF8.GetBytes(cache).Length >= 16000000) throw new ArgumentOutOfRangeException("Cache data is bigger than 16MB");
            Key = key;
            DataStorage = dataStorage;
            Buffer = cache;
            Type = DSValueType.STRING;
        }

        /// <summary>
        /// Create a buffer with <see cref="DSValueType.INTEGER"/> type
        /// </summary>
        /// <param name="cache">Value to be assign to <see cref="Buffer"/></param>
        /// <param name="key">Key in the Data Storage that has this value</param>
        /// <param name="dataStorage">Data Storage used to make simple operations</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="cache"/> has more than 16MB, this is throwed</exception>
        public DataStorageValue(int cache, string key, IGJDataStorage dataStorage)
        {
            if (Encoding.UTF8.GetBytes(cache.ToString()).Length >= 16000000) throw new ArgumentOutOfRangeException("Cache data is bigger than 16MB");
            Buffer = cache;
            Key = key;
            DataStorage = dataStorage;
            Type = DSValueType.INTEGER;
        }

        /// <summary>
        /// Collect value from <see cref="Buffer"/> in <c>string</c> type
        /// </summary>
        /// <param name="value"><see cref="DataStorageValue"/> to be converted</param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Buffer"/> isn't have <see cref="Type"/> <see cref="DSValueType.STRING"/></exception>
        public static implicit operator string(DataStorageValue value)
        {
            if (value.Type != DSValueType.STRING) throw new InvalidDataTypeException("DataStorageValue isn't has type string");
            return (string)value.Buffer;
        }

        /// <summary>
        /// Collect value from <see cref="Buffer"/> in <c>int</c> type
        /// </summary>
        /// <param name="value"><see cref="DataStorageValue"/> to be converted</param>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="Buffer"/> isn't have <see cref="Type"/> <see cref="DSValueType.INTEGER"/></exception>
        public static implicit operator int(DataStorageValue value)
        {
            if (value.Type != DSValueType.INTEGER) throw new InvalidDataTypeException("DataStorageValue isn't has type integer");
            return (int)value.Buffer;
        }

        /// <summary>
        /// Implicity execute <see cref="IGJDataStorage.Add(string, int)"/> and return result
        /// </summary>
        /// <param name="value"><see cref="Key"/> paramenter used in <see cref="IGJDataStorage.Add(string, int)"/></param>
        /// <param name="newvalue">Second paramenter of <see cref="IGJDataStorage.Add(string, int)"/></param>
        /// <returns>Result retrived from GameJolt Game API</returns>
        /// <exception cref="NoDataStorageException">Throwed if <see cref="DataStorageValue"/> doesn't has a <see cref="IGJDataStorage"/> linked</exception>
        /// <seealso cref="IGJDataStorage.Add(string, int)"/>
        public static DataStorageValue operator +(DataStorageValue value, int newvalue)
        {
            if (value.DataStorage == null) throw new NoDataStorageException("DataStorageValue doesn't has a IGJDataStorage linked");
            return value.DataStorage.Add(value.Key, newvalue);
        }

        /// <summary>
        /// Implicity execute <see cref="IGJDataStorage.Append(string, string)"/> and return result
        /// </summary>
        /// <param name="value"><see cref="Key"/> paramenter used in <see cref="IGJDataStorage.Append(string, string)"/></param>
        /// <param name="newvalue">Second paramenter of <see cref="IGJDataStorage.Append(string, string)"/></param>
        /// <returns>Result retrived from GameJolt Game API</returns>
        /// <exception cref="NoDataStorageException">Throwed if <see cref="DataStorageValue"/> doesn't has a <see cref="IGJDataStorage"/> linked</exception>
        /// <seealso cref="IGJDataStorage.Append(string, string)"/>
        public static DataStorageValue operator +(DataStorageValue value, string newvalue)
        {
            if (value.DataStorage == null) throw new NoDataStorageException("DataStorageValue doesn't has a IGJDataStorage linked");
            return value.DataStorage.Append(value.Key, newvalue);
        }

        /// <summary>
        /// Implicity execute <see cref="IGJDataStorage.Subtract(string, int)"/> and return result
        /// </summary>
        /// <param name="value"><see cref="Key"/> paramenter used in <see cref="IGJDataStorage.Subtract(string, int)"/></param>
        /// <param name="newvalue">Second paramenter of <see cref="IGJDataStorage.Subtract(string, int)"/></param>
        /// <returns>Result retrived from GameJolt Game API</returns>
        /// <exception cref="NoDataStorageException">Throwed if <see cref="DataStorageValue"/> doesn't has a <see cref="IGJDataStorage"/> linked</exception>
        /// <seealso cref="IGJDataStorage.Subtract(string, int)"/>
        public static DataStorageValue operator -(DataStorageValue value, int newvalue)
        {
            if (value.DataStorage == null) throw new NoDataStorageException("DataStorageValue doesn't has a IGJDataStorage linked");
            return value.DataStorage.Subtract(value.Key, newvalue);
        }

        /// <summary>
        /// Implicity execute <see cref="IGJDataStorage.Multiply(string, int)"/> and return result
        /// </summary>
        /// <param name="value"><see cref="Key"/> paramenter used in <see cref="IGJDataStorage.Multiply(string, int)"/></param>
        /// <param name="newvalue">Second paramenter of <see cref="IGJDataStorage.Multiply(string, int)"/></param>
        /// <returns>Result retrived from GameJolt Game API</returns>
        /// <exception cref="NoDataStorageException">Throwed if <see cref="DataStorageValue"/> doesn't has a <see cref="IGJDataStorage"/> linked</exception>
        /// <seealso cref="IGJDataStorage.Multiply(string, int)"/>
        public static DataStorageValue operator *(DataStorageValue value, int newvalue)
        {
            if (value.DataStorage == null) throw new NoDataStorageException("DataStorageValue doesn't has a IGJDataStorage linked");
            return value.DataStorage.Multiply(value.Key, newvalue);
        }

        /// <summary>
        /// Implicity execute <see cref="IGJDataStorage.Divide(string, int)"/> and return result
        /// </summary>
        /// <param name="value"><see cref="Key"/> paramenter used in <see cref="IGJDataStorage.Divide(string, int)"/></param>
        /// <param name="newvalue">Second paramenter of <see cref="IGJDataStorage.Divide(string, int)"/></param>
        /// <returns>Result retrived from GameJolt Game API</returns>
        /// <exception cref="NoDataStorageException">Throwed if <see cref="DataStorageValue"/> doesn't has a <see cref="IGJDataStorage"/> linked</exception>
        /// <seealso cref="IGJDataStorage.Divide(string, int)"/>
        public static DataStorageValue operator /(DataStorageValue value, int newvalue)
        {
            if (value.DataStorage == null) throw new NoDataStorageException("DataStorageValue doesn't has a IGJDataStorage linked");
            return value.DataStorage.Divide(value.Key, newvalue);
        }
    }
}
