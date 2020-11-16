using CodeReactor.CRGameJolt.Connector;
using System;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Manage a game session of a user in GameJolt
    /// </summary>
    /// <seealso cref="SessionStatus"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="WebCaller"/>
    public class SessionManager : IGJObject
    {
        /// <value>
        /// Set or get the <see cref="SessionStatus"/>
        /// </value>
        public SessionStatus Status { get; set; }

        /// <value>
        /// Is the user used to set the <see cref="Status"/>
        /// </value>
        public GameJoltMe User { get; set; }

        /// <value>
        /// A suggestive timeout between every <see cref="Update"/>
        /// </value>
        public const long Timeout = 30000;

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <summary>
        /// Initialize a new instance of <see cref="SessionManager"/> and open a new session in GameJolt Game API
        /// </summary>
        /// <param name="user">User that gonna be your session opened</param>
        /// <param name="status">Status used in <see cref="Ping"/> method</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public SessionManager(GameJoltMe user, SessionStatus status, WebCaller webCaller)
        {
            User = user;
            Status = status;
            WebCaller = webCaller;
            Update();
        }

        /// <summary>
        /// Same as <see cref="SessionManager(GameJoltMe, SessionStatus, WebCaller)"/> but using <see cref="SessionStatus.Active"/> as default
        /// </summary>
        /// <param name="user">User that gonna be your session opened</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public SessionManager(GameJoltMe user, WebCaller webCaller) : this(user, SessionStatus.Active, webCaller) { }

        /// <summary>
        /// Convert from a <see cref="SessionStatus"/> in a valid string used in <see cref="Ping"/>
        /// </summary>
        /// <param name="status"><see cref="SessionStatus"/> that gonna be converted</param>
        /// <exception cref="InvalidSessionStatusException">If a invalid <paramref name="status"/> is provided, this gonna be throwed</exception>
        /// <returns>A valid string that can be used in query string for session/ping endpoint</returns>
        public static string StatusToString(SessionStatus status)
        {
            switch (status)
            {
                case SessionStatus.Active:
                    return "active";
                case SessionStatus.Idle:
                    return "idle";
                default:
                    throw new InvalidSessionStatusException("Unknown session status");
            }
        }

        /// <summary>
        /// Open a new session in GameJolt Game API
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Open()
        {
            XElement response = WebCaller.GetAsXML("sessions/open", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <summary>
        /// Ping a existing session in GameJolt Game API and set your status using <see cref="Status"/> property
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <seealso cref="StatusToString(SessionStatus)"/>
        /// <seealso cref="Status"/>
        /// <seealso cref="SessionStatus"/>
        public void Ping()
        {
            XElement response = WebCaller.GetAsXML("sessions/ping", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken), "status="+StatusToString(Status) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <summary>
        /// Check if a session is opened in GameJolt Game API
        /// </summary>
        /// <returns>Is true if a session already exists or false if isn't exists</returns>
        public bool Check()
        {
            XElement response = WebCaller.GetAsXML("sessions/check", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            return (response.Element("success").Value != "true");
        }

        /// <summary>
        /// Close a existing session in GameJolt Game API
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Close()
        {
            XElement response = WebCaller.GetAsXML("sessions/close", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <inheritdoc/>
        public void Update()
        {
            if (Check())
            {
                Ping();
            } else
            {
                Open();
            }
        }
    }
}
