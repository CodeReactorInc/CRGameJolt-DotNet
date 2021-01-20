using CodeReactor.CRGameJolt.Connector;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// A thread safe List created using the friend list from a user in GameJolt
    /// </summary>
    /// <seealso cref="GameJoltMe"/>
    /// <seealso cref="GameJoltUser"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="Connector.WebCaller"/>
    public class FriendList : ConcurrentDictionary<int, GameJoltUser>, IGJObject
    {
        /// <value>
        /// User used to download friend list
        /// </value>
        public GameJoltMe User { get; set; }

        /// <summary>
        /// Create a new <see cref="FriendList"/> using a <see cref="GameJoltMe"/> to download a friend list
        /// </summary>
        /// <param name="user">User that contain the friend list</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public FriendList(GameJoltMe user, WebCaller webCaller) : base()
        {
            WebCaller = webCaller;
            User = user;
            Update();
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            Clear();
            XElement response = WebCaller.GetAsXML("friends", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            int i = 0;
            foreach (XElement friend in response.Element("friends").Elements("friend"))
            {
                try
                {
                    base[i] = new GameJoltUser(int.Parse(friend.Element("friend_id").Value), WebCaller);
                    i++;
                }
                catch (FormatException) { }
            }
        }
    }
}
