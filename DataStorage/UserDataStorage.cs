﻿using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// A threadsafe Dictionary synced with user Data Storage from GameJolt Game API
    /// </summary>
    /// <seealso cref="IGJDataStorage"/>
    /// <seealso cref="DataStorageValue"/>
    /// <seealso cref="GlobalDataStorage"/>
    /// <seealso cref="GameJoltMe"/>
    /// <seealso cref="Connector.WebCaller"/>
    public class UserDataStorage : IGJDataStorage
    {
        /// <inheritdoc/>
        public ConcurrentDictionary<string, DataStorageValue> Cache { get; private set; }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <value>
        /// User reference used to sync data from <see cref="Cache"/> to Data Storage
        /// </value>
        public GameJoltMe User { get; set; }

        /// <summary>
        /// Download and sync all data from a user Data Storage of GameJolt Game API on <see cref="Cache"/>
        /// </summary>
        /// <param name="user">User used to sync Data Storage</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        public UserDataStorage(GameJoltMe user, WebCaller webCaller)
        {
            User = user;
            Cache = new ConcurrentDictionary<string, DataStorageValue>();
            WebCaller = webCaller;
            Read();
        }

        /// <inheritdoc/>
        public DataStorageValue Add(string key, int value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Add(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public DataStorageValue Append(string key, string value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Append(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public DataStorageValue Divide(string key, int value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Divide(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public DataStorageValue Get(string key)
        {
            try {
                return Cache[key];
            } catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public DataStorageValue Multiply(string key, int value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Multiply(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public DataStorageValue Prepend(string key, string value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Prepend(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public void Read()
        {
            XElement response = WebCaller.GetAsXML("data-store/get-keys", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            foreach (XElement key in response.Element("keys").Elements("key"))
            {
                Read(key.Value);
            }
        }

        /// <inheritdoc/>
        public void Read(string key)
        {
            XElement response = WebCaller.GetAsXML("data-store", new string[] { "key=" + key, "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            int preintvalue;
            if (int.TryParse(response.Element("data").Value, out preintvalue))
            {
                Cache[key] = new DataStorageValue(preintvalue);
            }
            else
            {
                Cache[key] = new DataStorageValue(response.Element("data").Value);
            }
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            _ = Cache.TryRemove(key, out _);
            XElement response = WebCaller.GetAsXML("data-store/remove", new string[] { "key=" + WebUtility.UrlEncode(key), "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <inheritdoc/>
        public void Set(string key, DataStorageValue value)
        {
            Cache[key] = value;
        }

        /// <inheritdoc/>
        public void Set(string key, int value)
        {
            Cache[key] = new DataStorageValue(value);
        }

        /// <inheritdoc/>
        public void Set(string key, string value)
        {
            Cache[key] = new DataStorageValue(value);
        }

        /// <inheritdoc/>
        public DataStorageValue Subtract(string key, int value)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return null;
            tmp.Subtract(value);
            Cache[key] = tmp;
            return tmp;
        }

        /// <inheritdoc/>
        public void Update()
        {
            Read();
            Write();
        }

        /// <inheritdoc/>
        public void Write()
        {
            foreach (string key in Cache.Keys)
            {
                Write(key);
            }
        }

        /// <inheritdoc/>
        public void Write(string key)
        {
            DataStorageValue tmp = Get(key);
            if (tmp == null) return;
            string data;
            if (tmp.Type == DSValueType.INTEGER)
            {
                data = ((int)tmp).ToString();
            }
            else
            {
                data = tmp;
            }
            XElement response = WebCaller.GetAsXML("data-store/set", new string[] { "key=" + WebUtility.UrlEncode(key), "data=" + WebUtility.UrlEncode(data), "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}