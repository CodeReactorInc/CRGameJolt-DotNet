using CodeReactor.CRGameJolt.Connector;
using System;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Stored data about a GameJolt user
    /// </summary>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="UserType"/>
    /// <seealso cref="UserStatus"/>
    public class GameJoltUser : IGJObject
    {
        /// <value>
        /// User id downloaded from GameJolt Game API
        /// </value>
        public int Id { get; private set; }

        /// <value>
        /// Is the type of user inside of GameJolt
        /// </value>
        /// <seealso cref="UserType"/>
        public UserType Type { get; private set; }

        /// <value>
        /// The username used in GameJolt
        /// </value>
        public string Username { get; private set; }

        /// <value>
        /// The avatar URL of user in GameJolt
        /// </value>
        public string AvatarURL { get; private set; }

        /// <value>
        /// How long ago the user signed up in GameJolt
        /// </value>
        public DateTime SignedUp { get; private set; }

        /// <value>
        /// How long ago the user was last logged in
        /// </value>
        public DateTime LastLoggedIn { get; private set; }

        /// <value>
        /// The status about a user in GameJolt
        /// </value>
        /// <seealso cref="UserStatus"/>
        public UserStatus Status { get; private set; }

        /// <value>
        /// The user display name
        /// </value>
        public string DeveloperName { get; private set; }

        /// <value>
        /// The website of user or empty string if not specified
        /// </value>
        public string DeveloperWebsite { get; private set; }

        /// <value>
        /// The profile markdown description of user
        /// </value>
        public string DeveloperDescription { get; private set; }

        /// <summary>
        /// Request user data from GameJolt Game API using he username
        /// </summary>
        /// <param name="username">Username used to find user data</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public GameJoltUser(string username, WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("users", new string[] { "username=" + WebUtility.UrlEncode(username) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            XElement user = response.Element("users").Element("user");
            Id = int.Parse(user.Element("id").Value);
            Type = ConvertToUserType(user.Element("type").Value);
            Username = user.Element("username").Value;
            AvatarURL = user.Element("avatar_url").Value;
            SignedUp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("signed_up_timestamp").Value));
            LastLoggedIn = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("last_logged_in_timestamp").Value));
            Status = ConvertToUserStatus(user.Element("status").Value);
            DeveloperName = response.Element("developer_name").Value;
            DeveloperWebsite = response.Element("developer_website").Value;
            DeveloperDescription = user.Element("developer_description").Value;
        }

        /// <summary>
        /// Request user data from GameJolt Game API using he user id
        /// </summary>
        /// <param name="userid">User id used to find user data</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public GameJoltUser(int userid, WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("users", new string[] { "user_id=" + WebUtility.UrlEncode(userid.ToString()) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            XElement user = response.Element("users").Element("user");
            Id = int.Parse(user.Element("id").Value);
            Type = ConvertToUserType(user.Element("type").Value);
            Username = user.Element("username").Value;
            AvatarURL = user.Element("avatar_url").Value;
            SignedUp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("signed_up_timestamp").Value));
            LastLoggedIn = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("last_logged_in_timestamp").Value));
            Status = ConvertToUserStatus(user.Element("status").Value);
            DeveloperName = response.Element("developer_name").Value;
            DeveloperWebsite = response.Element("developer_website").Value;
            DeveloperDescription = user.Element("developer_description").Value;
        }

        /// <summary>
        /// Uses a string and convert he for a <see cref="UserType"/>
        /// </summary>
        /// <param name="usertype">String to be converted</param>
        /// <exception cref="ArgumentException">Throwed if <paramref name="usertype"/>is invalid and can't be converted</exception>
        /// <returns>The representation of string in <see cref="UserType"/> format</returns>
        public static UserType ConvertToUserType(string usertype)
        {
            switch (usertype)
            {
                case "User":
                    return UserType.User;
                case "Developer":
                    return UserType.Developer;
                case "Moderator":
                    return UserType.Moderator;
                case "Administrator":
                    return UserType.Administrator;
                default:
                    throw new ArgumentException("Invalid string, can't be converted");
            }
        }

        /// <summary>
        /// Uses a string and convert he for a <see cref="UserStatus"/>
        /// </summary>
        /// <param name="usertype">String to be converted</param>
        /// <exception cref="ArgumentException">Throwed if <paramref name="userstatus"/>is invalid and can't be converted</exception>
        /// <returns>The representation of string in <see cref="UserStatus"/> format</returns>
        public static UserStatus ConvertToUserStatus(string userstatus)
        {
            switch (userstatus)
            {
                case "Active":
                    return UserStatus.Active;
                case "Banned":
                    return UserStatus.Banned;
                default:
                    throw new ArgumentException("Invalid string, can't be converted");
            }
        }

        /// <inheritdoc/>
        public virtual WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public virtual void Update()
        {
            XElement response = WebCaller.GetAsXML("users", new string[] { "user_id=" + WebUtility.UrlEncode(Id.ToString()) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            XElement user = response.Element("users").Element("user");
            Id = int.Parse(user.Element("id").Value);
            Type = ConvertToUserType(user.Element("type").Value);
            Username = user.Element("username").Value;
            AvatarURL = user.Element("avatar_url").Value;
            SignedUp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("signed_up_timestamp").Value));
            LastLoggedIn = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(user.Element("last_logged_in_timestamp").Value));
            Status = ConvertToUserStatus(user.Element("status").Value);
            DeveloperName = response.Element("developer_name").Value;
            DeveloperWebsite = response.Element("developer_website").Value;
            DeveloperDescription = user.Element("developer_description").Value;
        }
    }
}
