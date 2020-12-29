using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.DataStorage;
using CodeReactor.CRGameJolt.Users.Trophies;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Represent a authed user in GameJolt Game API
    /// </summary>
    /// <seealso cref="GameJoltUser"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="FriendList"/>
    /// <seealso cref="WebCaller"/>
    /// <seealso cref="TrophiesManager"/>
    /// <seealso cref="UserDataStorage"/>
    /// <seealso cref="SessionManager"/>
    public class GameJoltMe : GameJoltUser
    {
        /// <value>
        /// User token used to login in GameJolt Game API
        /// </value>
        public string UserToken { get; private set; }

        /// <value>
        /// Private version of <see cref="Friends"/> but without a soft get
        /// </value>
        private FriendList _friends { get; set; }

        /// <value>
        /// Get or create a new <see cref="FriendList"/> using the internal <see cref="GameJoltUser.WebCaller"/>
        /// </value>
        public FriendList Friends
        {
            get
            {
                if (_friends == null)
                {
                    _friends = new FriendList(this, WebCaller);
                    return _friends;
                }
                else return _friends;
            }
            private set
            {
                _friends = value;
            }
        }

        /// <value>
        /// Private version of <see cref="Session"/> but without a soft get
        /// </value>
        private SessionManager _session { get; set; }

        /// <value>
        /// Get or create a new <see cref="SessionManager"/> using the internal <see cref="GameJoltUser.WebCaller"/>
        /// </value>
        public SessionManager Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new SessionManager(this, WebCaller);
                    return _session;
                }
                else return _session;
            }
            private set
            {
                _session = value;
            }
        }

        /// <value>
        /// Private version of <see cref="Trophies"/> but without a soft get
        /// </value>
        private TrophiesManager _trophies { get; set; }

        /// <value>
        /// Get or create a new <see cref="TrophiesManager"/> using the internal <see cref="GameJoltUser.WebCaller"/>
        /// </value>
        public TrophiesManager Trophies
        {
            get
            {
                if (_trophies == null)
                {
                    _trophies = new TrophiesManager(this, WebCaller);
                    return _trophies;
                }
                else return _trophies;
            }
            private set
            {
                _trophies = value;
            }
        }

        /// <value>
        /// Private version of <see cref="DataStorage"/> but without a soft get
        /// </value>
        private UserDataStorage _dataStorage { get; set; }

        /// <value>
        /// Get or create a new <see cref="UserDataStorage"/> using the internal <see cref="GameJoltUser.WebCaller"/>
        /// </value>
        public UserDataStorage DataStorage
        {
            get
            {
                if (_dataStorage == null)
                {
                    _dataStorage = new UserDataStorage(this, WebCaller);
                    return _dataStorage;
                }
                else return _dataStorage;
            }
            private set
            {
                _dataStorage = value;
            }
        }

        /// <summary>
        /// Login in a GameJolt account throught the GameJolt Game API using the <paramref name="username"/> and <paramref name="usertoken"/>
        /// </summary>
        /// <param name="username">Username from user URL, like https://gamejolt.com/@NatsumiUIX has username NatsumiUIX</param>
        /// <param name="usertoken">Game Token that can be getted from GameJolt site or .gj-credentials</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public GameJoltMe(string username, string usertoken, WebCaller webCaller) : base(username, webCaller)
        {
            UserToken = usertoken;
            XElement response = WebCaller.GetAsXML("users/auth", new string[] { "username=" + WebUtility.UrlEncode(Username), "user_token=" + WebUtility.UrlEncode(UserToken) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            if (_friends != null) _friends.Update();
            if (_session != null) _session.Update();
            if (_trophies != null) _trophies.Update();
        }
    }
}
