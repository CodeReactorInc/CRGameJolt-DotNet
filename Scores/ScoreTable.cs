using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Scores
{
    /// <summary>
    /// A GameJolt Game API score table controller
    /// </summary>
    /// <seealso cref="ScoreValue"/>
    /// <seealso cref="TableManager"/>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="GameJoltMe"/>
    public class ScoreTable : IGJObject
    {
        /// <value>
        /// The table id used in API calls
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Initialize a table using a table id and a <seealso cref="Connector.WebCaller"/>
        /// </summary>
        /// <param name="tableid">Table id getted from GameJolt Game API</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreTable(int tableid, WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=1", "table_id=" + tableid }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            Id = tableid;
        }

        /// <summary>
        /// Fetch score data about <paramref name="user"/>
        /// </summary>
        /// <param name="user">User used as reference to search score</param>
        /// <returns>Score data in <see cref="ScoreValue"/> format</returns>
        /// <seealso cref="ScoreValue(ScoreTable, GameJoltMe, WebCaller)"/>
        public ScoreValue Fetch(GameJoltMe user)
        {
            return new ScoreValue(this, user, WebCaller);
        }

        /// <summary>
        /// Fetch score data about <paramref name="guest"/>
        /// </summary>
        /// <param name="guest">Guest name used as reference to search score</param>
        /// <returns>Score data in <see cref="ScoreValue"/> format</returns>
        /// <seealso cref="ScoreValue(ScoreTable, string, WebCaller)"/>
        public ScoreValue Fetch(string guest)
        {
            return new ScoreValue(this, guest, WebCaller);
        }

        /// <summary>
        /// Fetch a bulk global score data
        /// </summary>
        /// <param name="limit">Size of score list</param>
        /// <returns>Scores based on sort direction</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if <paramref name="limit"/> is less than 0 or bigest than 100</exception>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreValue[] Fetch(int limit)
        {
            if (limit <= 0 || limit > 100) throw new ArgumentOutOfRangeException("Limit aren't in GameJolt Game API limit");
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=" + limit, "table_id=" + Id }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            List<ScoreValue> list = new List<ScoreValue>();
            foreach (XElement score in response.Element("scores").Elements("score"))
            {
                list.Add(new ScoreValue(score, this, WebCaller));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Fetch a bulk global score data based that are better than <paramref name="sort"/>
        /// </summary>
        /// <param name="sort">Sort used as reference</param>
        /// <param name="limit">Size of score list</param>
        /// <returns>Scores based on sort direction</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if <paramref name="limit"/> is less than 0 or bigest than 100</exception>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreValue[] BetterThan(int sort, int limit)
        {
            if (limit <= 0 || limit > 100) throw new ArgumentOutOfRangeException("Limit aren't in GameJolt Game API limit");
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=" + limit, "table_id=" + Id, "better_than=" + sort }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            List<ScoreValue> list = new List<ScoreValue>();
            foreach (XElement score in response.Element("scores").Elements("score"))
            {
                list.Add(new ScoreValue(score, this, WebCaller));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Fetch a bulk global score data based that are worse than <paramref name="sort"/>
        /// </summary>
        /// <param name="sort">Sort used as reference</param>
        /// <param name="limit">Size of score list</param>
        /// <returns>Scores based on sort direction</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if <paramref name="limit"/> is less than 0 or bigest than 100</exception>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreValue[] WorseThan(int sort, int limit)
        {
            if (limit <= 0 || limit > 100) throw new ArgumentOutOfRangeException("Limit aren't in GameJolt Game API limit");
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=" + limit, "table_id=" + Id, "worse_than=" + sort }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            List<ScoreValue> list = new List<ScoreValue>();
            foreach (XElement score in response.Element("scores").Elements("score"))
            {
                list.Add(new ScoreValue(score, this, WebCaller));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Add a score in the table
        /// </summary>
        /// <param name="score">Score in string format</param>
        /// <param name="sort">Score in integer format to create a sort order</param>
        /// <param name="extradata">Extra data to store, can be null</param>
        /// <param name="user">User that gonna receive score</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Add(string score, int sort, string extradata, GameJoltMe user)
        {
            XElement response;
            if (extradata == null)
            {
                response = WebCaller.GetAsXML("scores/add", new string[] { "score=" + score, "sort=" + sort, "username=" + user.Username, "user_token=" + user.UserToken, "table_id=" + Id }).Element("response");
            }
            else
            {
                response = WebCaller.GetAsXML("scores/add", new string[] { "score=" + score, "sort=" + sort, "username=" + user.Username, "user_token=" + user.UserToken, "extra_data=" + extradata, "table_id=" + Id }).Element("response");
            }
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <summary>
        /// Add a score in the table
        /// </summary>
        /// <param name="score">Score in string format</param>
        /// <param name="sort">Score in integer format to create a sort order</param>
        /// <param name="extradata">Extra data to store, can be null</param>
        /// <param name="guest">Guest name that gonna receive score</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Add(string score, int sort, string extradata, string guest)
        {
            XElement response;
            if (extradata == null)
            {
                response = WebCaller.GetAsXML("scores/add", new string[] { "score=" + score, "sort=" + sort, "guest=" + guest, "table_id=" + Id }).Element("response");
            }
            else
            {
                response = WebCaller.GetAsXML("scores/add", new string[] { "score=" + score, "sort=" + sort, "guest=" + guest, "extra_data=" + extradata, "table_id=" + Id }).Element("response");
            }
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <summary>
        /// Get the relative position of a sort in table
        /// </summary>
        /// <param name="sort">Sort to calculate the relative position</param>
        /// <returns>The relative position in table</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public int GetRank(int sort)
        {
            XElement response = WebCaller.GetAsXML("scores/get-rank", new string[] { "table_id=" + Id, "sort=" + sort }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return int.Parse(response.Element("rank").Value);
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=1", "table_id=" + Id }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}
