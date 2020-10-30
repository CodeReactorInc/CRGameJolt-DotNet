using CodeReactor.CRGameJolt.Connector;
using System.Collections.Concurrent;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Interface to a stardand GameJolt Data Storage
    /// </summary>
    /// <seealso cref="WebCaller"/>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="DataStorageValue"/>
    public interface IGJDataStorage : IGJObject
    {
        /// <value>
        /// A local cache of Data Storage copied from GameJolt Game API
        /// </value>
        ConcurrentDictionary<string, DataStorageValue> Cache { get; }

        /// <summary>
        /// Read all Data Storage from GameJolt Game API using default <see cref="WebCaller"/>
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Read();

        /// <summary>
        /// Write all <see cref="Cache"/> to GameJolt Game API using default <see cref="WebCaller"/>
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Write();

        /// <summary>
        /// Read all Data Storage from GameJolt Game API using a custom <see cref="WebCaller"/>
        /// </summary>
        /// <param name="webCaller">A custom <see cref="WebCaller"/> to use</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Read(WebCaller webCaller);

        /// <summary>
        /// Write all <see cref="Cache"/> to GameJolt Game API using a custom <see cref="WebCaller"/>
        /// </summary>
        /// <param name="webCaller">A custom <see cref="WebCaller"/> to use</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Write(WebCaller webCaller);

        /// <summary>
        /// Only read a key from Data Storage from GameJolt Game API using default <see cref="WebCaller"/>
        /// </summary>
        /// <param name="key">Key to read from Data Storage</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Read(string key);

        /// <summary>
        /// Only write a key from <see cref="Cache"/> to GameJolt Game API using default <see cref="WebCaller"/>
        /// </summary>
        /// <param name="key">Key to write from <see cref="Cache"/></param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Write(string key);

        /// <summary>
        /// Only read a key from Data Storage from GameJolt Game API using a custom <see cref="WebCaller"/>
        /// </summary>
        /// <param name="key">Key to read from Data Storage</param>
        /// <param name="webCaller">A custom <see cref="WebCaller"/> to use</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Read(string key, WebCaller webCaller);

        /// <summary>
        /// Only write a key from <see cref="Cache"/> to GameJolt Game API using a custom <see cref="WebCaller"/>
        /// </summary>
        /// <param name="key">Key to write from <see cref="Cache"/></param>
        /// <param name="webCaller">A custom <see cref="WebCaller"/> to use</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Write(string key, WebCaller webCaller);

        /// <summary>
        /// Remove a value from <see cref="Cache"/> and Data Storage using a key
        /// </summary>
        /// <param name="key">Key of value to remove</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Remove(string key);

        /// <summary>
        /// Remove a value from <see cref="Cache"/> and Data Storage using a key and a custom <see cref="WebCaller"/>
        /// </summary>
        /// <param name="key">Key of value to remove</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Remove(string key, WebCaller webCaller);

        /// <summary>
        /// Add or update a value on <see cref="Cache"/>
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to add or update</param>
        void Set(string key, DataStorageValue value);

        /// <summary>
        /// Add or update a value converting <c>int</c> to <see cref="DataStorageValue"/>
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to convert for a <see cref="DataStorageValue"/></param>
        void Set(string key, int value);

        /// <summary>
        /// Add or update a value converting <c>string</c> to <see cref="DataStorageValue"/>
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to convert for a <see cref="DataStorageValue"/></param>
        void Set(string key, string value);

        /// <summary>
        /// Get a value from cache that you can use implicit conversion
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <returns>Value from cache or a type null GJDataValue if key doesn't exists</returns>
        DataStorageValue Get(string key);

        /// <summary>
        /// Run <see cref="DataStorageValue.Add(int)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>int</c> to execute add</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Add(string key, int value);

        /// <summary>
        /// Run <see cref="DataStorageValue.Subtract(int)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>int</c> to execute subtract</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Subtract(string key, int value);

        /// <summary>
        /// Run <see cref="DataStorageValue.Multiply(int)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>int</c> to execute multiply</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Multiply(string key, int value);

        /// <summary>
        /// Run <see cref="DataStorageValue.Divide(int)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>int</c> to execute divide</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Divide(string key, int value);

        /// <summary>
        /// Run <see cref="DataStorageValue.Append(string)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>string</c> to append</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Append(string key, string value);

        /// <summary>
        /// Run <see cref="DataStorageValue.Prepend(string)"/> on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in <c>string</c> to preappend</param>
        /// <returns>Value updated or <c>null</c> if key doesn't exists</returns>
        DataStorageValue Prepend(string key, string value);
    }
}
