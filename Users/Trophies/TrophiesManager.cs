﻿using CodeReactor.CRGameJolt.Connector;
using System.Collections.Concurrent;
using System.Xml.Linq;
using System.Net;

namespace CodeReactor.CRGameJolt.Users.Trophies
{
    /// <summary>
    /// Manage all trophies from a game in GameJolt
    /// </summary>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="Trophy"/>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="GameJoltMe"/>
    public class TrophiesManager : ConcurrentDictionary<int, Trophy>, IGJObject
    {
        /// <value>
        /// Id used in <see cref="Update"/> to fetch information about the trophies
        /// </value>
        public GameJoltMe User { get; set; }

        /// <summary>
        /// Fetch all data about all trophies in the game in GameJolt
        /// </summary>
        /// <param name="user"></param>
        /// <param name="webCaller"></param>
        public TrophiesManager(GameJoltMe user, WebCaller webCaller)
        {
            User = user;
            WebCaller = webCaller;
            Update();
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            XElement response = WebCaller.GetAsXML("trophies", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            Clear();
            foreach (XElement trophy in response.Element("trophies").Elements("trophy"))
            {
                Trophy trophyObj = new Trophy(int.Parse(trophy.Element("id").Value), User, WebCaller);
                base[trophyObj.Id] = trophyObj;
            }
        }
    }
}