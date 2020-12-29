using CodeReactor.CRGameJolt.Connector;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Interface to a stardand GameJolt Data Storage
    /// </summary>
    /// <seealso cref="WebCaller"/>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="UserDataStorage"/>
    /// <seealso cref="DataStorageValue"/>
    public interface IGJDataStorage : IGJObject
    {
        /// <param name="key">Key used in Data Storage</param>
        /// <returns>Data retrived from Data Storage</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        DataStorageValue this[string key] { get; set; }

        /// <value>
        /// All keys available and valid in Data Storage
        /// </value>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        string[] Keys { get; }

        /// <summary>
        /// Remove a value from Data Storage using a key
        /// </summary>
        /// <param name="key">The key used to remove</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Remove(string key);

        /// <value>
        /// The count of values inside of Game Jolt Data Storage
        /// </value>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        int Count { get; }

        /// <summary>
        /// Send a update request with add operation and make a sum in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.INTEGER"/></exception>
        DataStorageValue Add(string key, int newvalue);

        /// <summary>
        /// Send a update request with subtract operation and make a subtract in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.INTEGER"/></exception>
        DataStorageValue Subtract(string key, int newvalue);

        /// <summary>
        /// Send a update request with multiply operation and make a multiply in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.INTEGER"/></exception>
        DataStorageValue Multiply(string key, int newvalue);

        /// <summary>
        /// Send a update request with divide operation and make a divide in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.INTEGER"/></exception>
        DataStorageValue Divide(string key, int newvalue);

        /// <summary>
        /// Send a update request with append operation and make a append in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.STRING"/></exception>
        DataStorageValue Append(string key, string newvalue);

        /// <summary>
        /// Send a update request with prepend operation and make a prepend in value
        /// </summary>
        /// <param name="key">The key to be updated</param>
        /// <param name="newvalue">New value provided for Data Storage with the operation</param>
        /// <returns>The value updated</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="InvalidDataTypeException">Throwed if <see cref="DataStorageValue.Type"/> is a <see cref="DSValueType.STRING"/></exception>
        DataStorageValue Prepend(string key, string newvalue);
    }
}
