using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A Dictionary that download and upload data for GameJolt User Data Storage
    /// </summary>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="DataStorageValue"/>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="GameJoltMe"/>
    /// <seealso cref="Connector.WebCaller"/>
    public class UserDataStorage : IGJDataStorage
    {
        /// <value>
        /// Id used to sync and locate the GameJolt User Data Storage
        /// </value>
        public GameJoltMe User { get; set; }

        /// <summary>
        /// Initialize <see cref="UserDataStorage"/> connected with Data Storage inside of <paramref name="webCaller"/> using <paramref name="user"/>
        /// </summary>
        /// <param name="user">User that gonna be used in sync</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        public UserDataStorage(GameJoltMe user, WebCaller webCaller)
        {
            User = user;
            WebCaller = webCaller;
        }

        /// <inheritdoc/>
        public DataStorageValue this[string key]
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store", new string[] { "key=" + WebUtility.UrlEncode(key), "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                return DataStorageValue.Parse(response.Element("data").Value, key, this);
            }
            set
            {
                string rawdata;
                if (value.Type == DSValueType.INTEGER)
                {
                    rawdata = ((int)value).ToString();
                }
                else
                {
                    rawdata = value;
                }
                XElement response = WebCaller.GetAsXML("data-store/set", new string[] { "key=" + WebUtility.UrlEncode(key), "data=" + WebUtility.UrlEncode(rawdata), "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            }
        }

        /// <inheritdoc/>
        public string[] Keys
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                int size = 0;
                foreach (XElement key in response.Element("keys").Elements("key"))
                {
                    size++;
                }
                int index = 0;
                string[] data = new string[size];
                foreach (XElement key in response.Element("keys").Elements("key"))
                {
                    data[index] = key.Element("key").Value;
                    index++;
                }
                return data;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                int size = 0;
                foreach (XElement key in response.Element("keys").Elements("key"))
                {
                    size++;
                }
                return size;
            }
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public DataStorageValue Add(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=add", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Append(string key, string newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue), "operation=append", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Divide(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=divide", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Multiply(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=multiply", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Prepend(string key, string newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue), "operation=prepend", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            XElement response = WebCaller.GetAsXML("data-store/remove", new string[] { "key=" + WebUtility.UrlEncode(key), "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <inheritdoc/>
        public DataStorageValue Subtract(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=subtract", "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public void Update()
        {

        }
    }
}
