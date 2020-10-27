using CodeReactor.CRGameJolt.Connector;
using System.Collections.Concurrent;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Interface to stardand DataStorage for all types of 
    /// </summary>
    public interface IGJDataStorage : IGJObject
    {
        /// <value>
        /// A local cache of DataStorage copied from GameJolt Game API
        /// </value>
        ConcurrentDictionary<string, GJDataValue> Cache { get; }

        /// <summary>
        /// Read all DataStorage from GameJolt Game API using default WebCaller
        /// </summary>
        void Read();

        /// <summary>
        /// Write all cache to GameJolt Game API using default WebCaller
        /// </summary>
        void Write();

        /// <summary>
        /// Read all DataStorage from GameJolt Game API using a custom WebCaller
        /// </summary>
        /// <param name="webCaller">A custom WebCaller to use</param>
        void Read(WebCaller webCaller);

        /// <summary>
        /// Write all cache to GameJolt Game API using a custom WebCaller
        /// </summary>
        /// <param name="webCaller">A custom WebCaller to use</param>
        void Write(WebCaller webCaller);

        /// <summary>
        /// Only read a key from DataStorage from GameJolt Game API using default WebCaller
        /// </summary>
        /// <param name="key">Key to read from DataStorage</param>
        void Read(string key);

        /// <summary>
        /// Only write a key from cache to GameJolt Game API using default WebCaller
        /// </summary>
        /// <param name="key">Key to write from cache</param>
        void Write(string key);

        /// <summary>
        /// Only read a key from DataStorage from GameJolt Game API using a custom WebCaller
        /// </summary>
        /// <param name="key">Key to read from DataStorage</param>
        /// <param name="webCaller">A custom WebCaller to use</param>
        void Read(string key, WebCaller webCaller);

        /// <summary>
        /// Only write a key from cache to GameJolt Game API using a custom WebCaller
        /// </summary>
        /// <param name="key">Key to write from cache</param>
        /// <param name="webCaller">A custom WebCaller to use</param>
        void Write(string key, WebCaller webCaller);

        /// <summary>
        /// Remove a value from cache and DataStorage using a key
        /// </summary>
        /// <param name="key">Key of value to remove</param>
        void Remove(string key);

        /// <summary>
        /// Remove a value from cache and DataStorage using a key and a custom WebCaller
        /// </summary>
        /// <param name="key">Key of value to remove</param>
        void Remove(string key, WebCaller webCaller);

        /// <summary>
        /// Add or update a value on cache
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to add or update</param>
        void Set(string key, GJDataValue value);

        /// <summary>
        /// Add or update a value on cache without conversion
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to convert for a GJDataValue</param>
        void Set(string key, int value);

        /// <summary>
        /// Add or update a value on cache without conversion
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value to convert for a GJDataValue</param>
        void Set(string key, string value);

        /// <summary>
        /// Get a value from cache that you can use implicit conversion
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <returns>Value from cache or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Get(string key);

        /// <summary>
        /// Run GJDataValue.Add on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in int to add</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Add(string key, int value);

        /// <summary>
        /// Run GJDataValue.Subtract on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in int to subtract</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Subtract(string key, int value);

        /// <summary>
        /// Run GJDataValue.Multiply on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in int to multiply</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Multiply(string key, int value);

        /// <summary>
        /// Run GJDataValue.Divide on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in int to divide</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Divide(string key, int value);

        /// <summary>
        /// Run GJDataValue.Append on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in string to append</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Append(string key, string value);

        /// <summary>
        /// Run GJDataValue.Preappend on a key, save and return the result
        /// </summary>
        /// <param name="key">The key of value</param>
        /// <param name="value">Value in string to preappend</param>
        /// <returns>Value updated or a type null GJDataValue if key doesn't exists</returns>
        GJDataValue Prepend(string key, string value);
    }
}
