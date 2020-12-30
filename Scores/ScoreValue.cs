using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;
using System;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Scores
{
    /// <summary>
    /// Fetched result of a score inside of GameJolt Game API
    /// </summary>
    /// <seealso cref="ScoreElementNotFoundException"/>
    /// <seealso cref="ScoreTable"/>
    /// <seealso cref="TableManager"/>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="GameJoltMe"/>
    public class ScoreValue : IGJObject
    {
        /// <value>
        /// The score value in string format
        /// </value>
        public string Score { get; private set; }

        /// <value>
        /// The score value in integer format
        /// </value>
        public int Sort { get; private set; }

        /// <value>
        /// Extra data stored inside of score
        /// </value>
        public string ExtraData { get; private set; }

        /// <value>
        /// User name if has any, if hasn't, is null
        /// </value>
        public string User { get; private set; }

        /// <value>
        /// User ID if has any, if hasn't, is zero
        /// </value>
        public int UserId { get; private set; }

        /// <value>
        /// User guest name if has any, if hasn't, is null
        /// </value>
        public string Guest { get; private set; }

        /// <value>
        /// The date of score register
        /// </value>
        public DateTime Stored { get; private set; }

        /// <value>
        /// Score table associated
        /// </value>
        public ScoreTable Table { get; private set; }

        /// <value>
        /// The user associated with this <see cref="ScoreValue"/>
        /// </value>
        public GameJoltMe Me { get; private set; }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <summary>
        /// Fetch score from a guest in <paramref name="table"/>
        /// </summary>
        /// <param name="table">Table used to fetch data</param>
        /// <param name="guest">Guest name with the score</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreValue(ScoreTable table, string guest, WebCaller webCaller)
        {
            Table = table;
            WebCaller = webCaller;
            Guest = guest;
            Me = null;
            Update();
        }

        /// <summary>
        /// Fetch the user score using a <see cref="GameJoltMe"/> 
        /// </summary>
        /// <param name="table">Table used to fetch data</param>
        /// <param name="me">A user logged in GameJolt Game API</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreValue(ScoreTable table, GameJoltMe me, WebCaller webCaller)
        {
            Table = table;
            WebCaller = webCaller;
            Guest = null;
            Me = me;
            Update();
        }

        /// <summary>
        /// Parse a raw XML and initialize a <see cref="ScoreValue"/>
        /// </summary>
        /// <param name="score">Raw score value in XML to be parsed</param>
        /// <param name="table">Table used to fetch data</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        /// <exception cref="ScoreElementNotFoundException">Throwed if a necessary element to parse doesn't exists</exception>
        public ScoreValue(XElement score, ScoreTable table, WebCaller webCaller)
        {
            WebCaller = webCaller;

            if (score.Element("score") == null) throw new ScoreElementNotFoundException("score.id doesn't exists");
            if (score.Element("sort") == null) throw new ScoreElementNotFoundException("sort.title doesn't exists");
            if (score.Element("extra_data") == null) throw new ScoreElementNotFoundException("score.extra_data doesn't exists");
            if (score.Element("user") == null) throw new ScoreElementNotFoundException("score.user doesn't exists");
            if (score.Element("guest") == null) throw new ScoreElementNotFoundException("score.guest doesn't exists");

            if (!int.TryParse(score.Element("user_id").Value, out _) && string.IsNullOrWhiteSpace(score.Element("guest").Value)) throw new ScoreElementNotFoundException("score.user_id isn't a int");
            if (!int.TryParse(score.Element("stored_timestamp").Value, out _)) throw new ScoreElementNotFoundException("score.stored_timestamp ins't a int");

            if (string.IsNullOrWhiteSpace(score.Element("guest").Value))
            {
                User = score.Element("user").Value;
                UserId = int.Parse(score.Element("user_id").Value);
                Guest = null;
            }
            else
            {
                Guest = score.Element("guest").Value;
                User = "";
                UserId = 0;
            }
            Sort = int.Parse(score.Element("sort").Value);
            Score = score.Element("score").Value;
            ExtraData = score.Element("extra_data").Value;
            Stored = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(score.Element("stored_timestamp").Value));
        }

        /// <inheritdoc/>
        public void Update()
        {
            if (Guest == null && Me == null) return;
            XElement response;
            if (Guest == null)
            {
                response = WebCaller.GetAsXML("scores", new string[] { "username=" + WebUtility.UrlEncode(Me.Username), "user_token=" + WebUtility.UrlEncode(Me.UserToken), "table_id=" + WebUtility.UrlEncode(Table.Id.ToString()), "limit=1" }).Element("response");
            }
            else
            {
                response = WebCaller.GetAsXML("scores", new string[] { "guest=" + WebUtility.UrlEncode(Guest), "table_id=" + WebUtility.UrlEncode(Table.Id.ToString()), "limit=1" }).Element("response");
            }
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            XElement score = response.Element("scores").Element("score");
            if (Guest == null)
            {
                User = score.Element("user").Value;
                UserId = int.Parse(score.Element("user_id").Value);
            }
            else
            {
                Guest = score.Element("guest").Value;
            }
            Sort = int.Parse(score.Element("sort").Value);
            Score = score.Element("score").Value;
            ExtraData = score.Element("extra_data").Value;
            Stored = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(score.Element("stored_timestamp").Value));
        }
    }
}
