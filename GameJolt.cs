using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.DataStorage;
using CodeReactor.CRGameJolt.Scores;
using CodeReactor.CRGameJolt.Users;
using System;
using System.IO;

namespace CodeReactor.CRGameJolt
{
    public class GameJolt
    {
        public WebCaller WebCaller { get; private set; }

        public GameJoltMe UserLogged { get; private set; }

        private GlobalDataStorage _globalDataStorage { get; set; }

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

        private UserDataStorage _userDataStorage { get; set; }

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

        private TableManager _tables { get; set; }

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

        public GameJolt(string gameId, string gameKey)
        {
            WebCaller = new WebCaller(new URLConstructor(gameId, gameKey));
            UserLogged = null;
            _globalDataStorage = null;
            _userDataStorage = null;
            _tables = null;
        }

        public GameJoltUser FetchUser(string username)
        {
            return new GameJoltUser(username, WebCaller);
        }

        public GameJoltUser FetchUser(int userid)
        {
            return new GameJoltUser(userid, WebCaller);
        }

        public void Login(string username, string usertoken)
        {
            UserLogged = new GameJoltMe(username, usertoken, WebCaller);
        }

        public void LoginFromFile(Stream file)
        {
            StreamReader streamReader = new StreamReader(file);
            _ = streamReader.ReadLine();
            string username = streamReader.ReadLine();
            string usertoken = streamReader.ReadLine();
            if (username == null || usertoken == null) throw new ArgumentException("file isn't a valid .gj-credentials");
            UserLogged = new GameJoltMe(username, usertoken, WebCaller);
        }

        public void LoginFromFile(string path)
        {
            LoginFromFile(new FileStream(path, FileMode.Open););
        }
    }
}
