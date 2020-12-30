using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.DataStorage;
using CodeReactor.CRGameJolt.Scores;
using CodeReactor.CRGameJolt.Users;
using System;
using System.IO;

namespace CodeReactor.CRGameJolt
{
    /// <summary>
    /// General controller of all instances and GameJolt Game API
    /// </summary>
    /// <seealso cref="DataStorage.GlobalDataStorage"/>
    /// <seealso cref="DataStorage.UserDataStorage"/>
    /// <seealso cref="TableManager"/>
    /// <seealso cref="GameJoltMe"/>
    /// <seealso cref="GameJoltUser"/>
    /// <seealso cref="Connector.WebCaller"/>
    public class GameJolt
    {
        /// <value>
        /// A <see cref="Connector.WebCaller"/> used to create all other instances
        /// </value>
        public WebCaller WebCaller { get; private set; }

        /// <value>
        /// The user logged in GameJolt Game API
        /// </value>
        public GameJoltMe UserLogged { get; private set; }

        /// <value>
        /// Private version of <see cref="GlobalDataStorage"/> but without a soft get
        /// </value>
        private GlobalDataStorage _globalDataStorage { get; set; }

        /// <value>
        /// The Global Data Storage of the game
        /// </value>
        /// <seealso cref="DataStorage.GlobalDataStorage"/>
        public GlobalDataStorage GlobalDataStorage
        {
            get
            {
                if (_globalDataStorage == null)
                {
                    _globalDataStorage = new GlobalDataStorage(WebCaller);
                    return _globalDataStorage;
                }
                else
                {
                    _globalDataStorage.Update();
                    return _globalDataStorage;
                }
            }
        }

        /// <value>
        /// Private version of <see cref="UserDataStorage"/> but without a soft get
        /// </value>
        private UserDataStorage _userDataStorage { get; set; }

        /// <value>
        /// The user data storage associated with <see cref="UserLogged"/>
        /// </value>
        /// <exception cref="UserNotLoggedException">Throwed if no user are logged in Game API</exception>
        /// <seealso cref="DataStorage.UserDataStorage"/>
        public UserDataStorage UserDataStorage
        {
            get
            {
                if (_userDataStorage == null)
                {
                    if (UserLogged == null) throw new UserNotLoggedException("User aren't logged in");
                    _userDataStorage = new UserDataStorage(UserLogged, WebCaller);
                    return _userDataStorage;
                }
                else
                {
                    _userDataStorage.Update();
                    return _userDataStorage;
                }
            }
        }

        /// <value>
        /// Private version of <see cref="Tables"/> but without a soft get
        /// </value>
        private TableManager _tables { get; set; }

        /// <value>
        /// Control and manage all score tables of the game
        /// </value>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public TableManager Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = new TableManager(WebCaller);
                    return _tables;
                }
                else
                {
                    return _tables;
                }
            }
        }

        /// <summary>
        /// Initialize a clear <see cref="GameJolt"/> instance only with <see cref="Connector.WebCaller"/>
        /// </summary>
        /// <param name="gameId">Game id get from API Settings in GameJolt</param>
        /// <param name="gameKey">Game key get from API Settings in GameJolt</param>
        public GameJolt(string gameId, string gameKey)
        {
            WebCaller = new WebCaller(new URLConstructor(gameId, gameKey));
            UserLogged = null;
            _globalDataStorage = null;
            _userDataStorage = null;
            _tables = null;
        }

        /// <summary>
        /// Fetch user from GameJolt Game API using he username
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>The user parsed in a <see cref="GameJoltUser"/></returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public GameJoltUser FetchUser(string username)
        {
            return new GameJoltUser(username, WebCaller);
        }

        /// <summary>
        /// Fetch user from GameJolt Game API using he user id
        /// </summary>
        /// <param name="username">User id of the user</param>
        /// <returns>The user parsed in a <see cref="GameJoltUser"/></returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public GameJoltUser FetchUser(int userid)
        {
            return new GameJoltUser(userid, WebCaller);
        }

        /// <summary>
        /// Login the user using the username and the user token
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="usertoken">User token code used to access Game API</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Login(string username, string usertoken)
        {
            if (UserLogged == null) UserLogged = new GameJoltMe(username, usertoken, WebCaller);
        }

        /// <summary>
        /// Login the user using a Stream from .gj-credentials file
        /// </summary>
        /// <param name="file">A stream connected with .gj-credentials</param>
        /// <exception cref="ArgumentException">If the file aren't on correct format, this will be throwed</exception>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void LoginFromFile(Stream file)
        {
            StreamReader streamReader = new StreamReader(file);
            _ = streamReader.ReadLine();
            string username = streamReader.ReadLine();
            string usertoken = streamReader.ReadLine();
            if (username == null || usertoken == null) throw new ArgumentException("file isn't a valid .gj-credentials");
            if (UserLogged == null) UserLogged = new GameJoltMe(username, usertoken, WebCaller);
        }

        /// <summary>
        /// Login the user using the path of .gj-credentials file
        /// </summary>
        /// <param name="path">The path of .gj-credentials file</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void LoginFromFile(string path)
        {
            LoginFromFile(new FileStream(path, FileMode.Open));
        }
    }
}
