using CodeReactor.CRGameJolt.Connector;
using System;
using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Users.Trophies
{
    /// <summary>
    /// A single trophy representation with <see cref="Add"/> and <see cref="Remove"/> operations
    /// </summary>
    /// <seealso cref="TrophyDifficulty"/>
    /// <seealso cref="GameJoltMe"/>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="IGJObject"/>
    /// <seealso cref="TrophiesManager"/>
    public class Trophy : IGJObject
    {
        /// <value>
        /// Id used in <see cref="Update"/> to fetch information about trophy
        /// </value>
        public int Id { get; private set; }

        /// <value>
        /// The title of trophy
        /// </value>
        public string Title { get; private set; }

        /// <value>
        /// The trophy description
        /// </value>
        public string Description { get; private set; }

        /// <value>
        /// Trophy difficulty represented by a <see cref="TrophyDifficulty"/>
        /// </value>
        public TrophyDifficulty Difficulty { get; private set; }

        /// <value>
        /// The icon of trophy in URL format
        /// </value>
        public string ImageURL { get; private set; }

        /// <value>
        /// Is true if the <see cref="User"/> already has the trophy
        /// </value>
        public bool Achived { get; private set; }

        /// <value>
        /// Is the user used to verify if the user already has the trophy
        /// </value>
        public GameJoltMe User { get; private set; }

        /// <summary>
        /// Fetch information about a single trophy in GameJolt
        /// </summary>
        /// <param name="id">Id of trophy inside of GameJolt Game API</param>
        /// <param name="user">User required for trophy info fetch</param>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public Trophy(int id, GameJoltMe user, WebCaller webCaller)
        {
            Id = id;
            User = user;
            WebCaller = webCaller;
            Update();
        }

        /// <summary>
        /// Give the trophy to <see cref="User"/>
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Add()
        {
            XElement response = WebCaller.GetAsXML("trophies/add-achieved", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken), "trophy_id=" + WebUtility.UrlEncode(Id.ToString()) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            Update();
        }

        /// <summary>
        /// Remove the trophy from <see cref="User"/>
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public void Remove()
        {
            XElement response = WebCaller.GetAsXML("trophies/remove-achieved", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken), "trophy_id=" + WebUtility.UrlEncode(Id.ToString()) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            Update();
        }

        /// <summary>
        /// Convert from <c>string</c> to a <see cref="TrophyDifficulty"/>
        /// </summary>
        /// <param name="trophyDifficulty">String used in convertion</param>
        /// <returns>A valid <see cref="TrophyDifficulty"/></returns>
        /// <exception cref="InvalidTrophDifficultyException">Throwed if a invalid <paramref name="trophyDifficulty"/> is provided</exception>
        public static TrophyDifficulty StringToTrophyDifficulty(string trophyDifficulty)
        {
            switch(trophyDifficulty)
            {
                case "Bronze":
                    return TrophyDifficulty.Bronze;
                case "Silver":
                    return TrophyDifficulty.Silver;
                case "Gold":
                    return TrophyDifficulty.Gold;
                case "Platinum":
                    return TrophyDifficulty.Platinum;
                default:
                    throw new InvalidTrophDifficultyException("Invalid trophy difficult provided");
            }
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            XElement response = WebCaller.GetAsXML("trophies", new string[] { "username=" + WebUtility.UrlEncode(User.Username), "user_token=" + WebUtility.UrlEncode(User.UserToken), "trophy_id=" + WebUtility.UrlEncode(Id.ToString()) }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            XElement trophy = response.Element("trophies").Element("trophy");
            Id = int.Parse(trophy.Element("id").Value);
            Title = trophy.Element("title").Value;
            Description = trophy.Element("description").Value;
            ImageURL = trophy.Element("image_url").Value;
            Difficulty = StringToTrophyDifficulty(trophy.Element("difficulty").Value);
            Achived = trophy.Element("achived").Value != "false";
        }
    }
}
