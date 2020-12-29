using CodeReactor.CRGameJolt.Connector;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A Dictionary that download and upload data for GameJolt Global Data Storage
    /// </summary>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="DataStorageValue"/>
    /// <seealso cref="UserDataStorage"/>
    /// <seealso cref="Connector.WebCaller"/>
    public class GlobalDataStorage : IGJDataStorage
    {
        /// <summary>
        /// Initialize <see cref="GlobalDataStorage"/> connected with Data Storage inside of <paramref name="webCaller"/>
        /// </summary>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        public GlobalDataStorage(WebCaller webCaller)
        {
            WebCaller = webCaller;
        }

        /// <inheritdoc/>
        public DataStorageValue this[string key]
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store", new string[] { "key=" + WebUtility.UrlEncode(key) }).Element("response");
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
                XElement response = WebCaller.GetAsXML("data-store/set", new string[] { "key=" + WebUtility.UrlEncode(key), "data=" + WebUtility.UrlEncode(rawdata) }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            }
        }

        /// <inheritdoc/>
        public string[] Keys
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { }).Element("response");
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
                }
                return data;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { }).Element("response");
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
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=add" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Append(string key, string newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue), "operation=append" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Divide(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=divide" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Multiply(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=multiply" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public DataStorageValue Prepend(string key, string newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue), "operation=prepend" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            XElement response = WebCaller.GetAsXML("data-store/remove", new string[] { "key=" + WebUtility.UrlEncode(key) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <inheritdoc/>
        public DataStorageValue Subtract(string key, int newvalue)
        {
            XElement response = WebCaller.GetAsXML("data-store/update", new string[] { "key=" + WebUtility.UrlEncode(key), "value=" + WebUtility.UrlEncode(newvalue.ToString()), "operation=subtract" }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return DataStorageValue.Parse(response.Element("data").Value, key, this);
        }

        /// <inheritdoc/>
        public void Update()
        {

        }
    }
}
