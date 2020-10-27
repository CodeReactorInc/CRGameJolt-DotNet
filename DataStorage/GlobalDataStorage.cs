using CodeReactor.CRGameJolt.Connector;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A threadsafe Dictionary synced with Global DataStorage from GameJolt Game API
    /// </summary>
    /// <inheritdoc/>
    public class GlobalDataStorage : IGJDataStorage
    {
        public ConcurrentDictionary<string, GJDataValue> Cache { get; private set; }

        public WebCaller WebCaller { get; set; }

        /// <summary>
        /// Download and sync all from Global Data Storage of GameJolt Game API on cache
        /// </summary>
        /// <param name="webCaller">WebCaller with a</param>
        public GlobalDataStorage(WebCaller webCaller)
        {
            Cache = new ConcurrentDictionary<string, GJDataValue>();
            WebCaller = webCaller;
            Read();
        }

        public GJDataValue Add(string key, int value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Add(value);
            Cache[key] = tmp;
            return tmp;
        }

        public GJDataValue Append(string key, string value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Append(value);
            Cache[key] = tmp;
            return tmp;
        }

        public GJDataValue Divide(string key, int value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Divide(value);
            Cache[key] = tmp;
            return tmp;
        }

        public GJDataValue Get(string key)
        {
            return Cache[key];
        }

        public GJDataValue Multiply(string key, int value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Multiply(value);
            Cache[key] = tmp;
            return tmp;
        }

        public GJDataValue Prepend(string key, string value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Prepend(value);
            Cache[key] = tmp;
            return tmp;
        }

        public void Read()
        {
            Read(WebCaller);
        }

        public void Read(WebCaller webCaller)
        {
            XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            foreach (XElement key in response.Element("keys").Elements("key"))
            {
                Read(key.Value, webCaller);
            }
        }

        public void Read(string key)
        {
            Read(key, WebCaller);
        }

        public void Read(string key, WebCaller webCaller)
        {
            XElement response = webCaller.GetAsXML("data-store", new string[] { "key=" + key }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            int preintvalue;
            if (int.TryParse(response.Element("data").Value, out preintvalue))
            {
                Cache[key] = new GJDataValue(preintvalue);
            }
            else
            {
                Cache[key] = new GJDataValue(response.Element("data").Value);
            }
        }

        public void Remove(string key)
        {
            Remove(key, WebCaller);
        }

        public void Remove(string key, WebCaller webCaller)
        {
            GJDataValue tmp;
            Cache.TryRemove(key, out tmp);
            XElement response = webCaller.GetAsXML("data-store/remove", new string[] { "key=" + WebUtility.UrlEncode(key) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        public void Set(string key, GJDataValue value)
        {
            Cache[key] = value;
        }

        public void Set(string key, int value)
        {
            Cache[key] = new GJDataValue(value);
        }

        public void Set(string key, string value)
        {
            Cache[key] = new GJDataValue(value);
        }

        public GJDataValue Subtract(string key, int value)
        {
            GJDataValue tmp = Cache[key];
            tmp.Subtract(value);
            Cache[key] = tmp;
            return tmp;
        }

        public void Update(WebCaller webCaller)
        {
            Read(webCaller);
            Write(webCaller);
        }

        public void Update()
        {
            Update(WebCaller);
        }

        public void Write()
        {
            Write(WebCaller);
        }

        public void Write(WebCaller webCaller)
        {
            foreach (string key in Cache.Keys)
            {
                try
                {
                    Write(key, webCaller);
                } catch (ArgumentNullException) { }
            }
        }

        public void Write(string key)
        {
            Write(key, WebCaller);
        }

        public void Write(string key, WebCaller webCaller)
        {
            GJDataValue tmp = Cache[key];
            if (tmp.Type == GJDataType.NULL) throw new ArgumentNullException("GJDataValue has type null");
            string data;
            if (tmp.Type == GJDataType.INTEGER)
            {
                data = ((int)tmp).ToString();
            } else
            {
                data = tmp;
            }
            XElement response = webCaller.GetAsXML("data-store/set", new string[] { "key=" + WebUtility.UrlEncode(key), "data=" + WebUtility.UrlEncode(data) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}
