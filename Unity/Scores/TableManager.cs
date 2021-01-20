using CodeReactor.CRGameJolt.Connector;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Scores
{
    /// <summary>
    /// Get and manage all score tables from a game in GameJolt Game API
    /// </summary>
    /// <seealso cref="ScoreTable"/>
    /// <seealso cref="ScoreValue"/>
    /// <seealso cref="Connector.WebCaller"/>
    /// <seealso cref="IGJObject"/>
    public class TableManager : IGJObject
    {
        /// <param name="tableid">Id of the table that you want get</param>
        /// <returns>The table or if doesn't exists, null</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreTable this[int tableid]
        {
            get
            {
                XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                ScoreTable result = null;
                foreach (XElement table in response.Element("tables").Elements("table"))
                {
                    if (table.Element("id").Value == tableid.ToString())
                    {
                        result = new ScoreTable(int.Parse(table.Element("id").Value), WebCaller);
                        break;
                    }
                }
                return result;
            }
        }

        /// <value>
        /// Get and download all tables from GameJolt Game API
        /// </value>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreTable[] Tables
        {
            get
            {
                XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                List<ScoreTable> list = new List<ScoreTable>();
                foreach (XElement table in response.Element("tables").Elements("table"))
                {
                    list.Add(new ScoreTable(int.Parse(table.Element("id").Value), WebCaller));
                }
                return list.ToArray();
            }
        }

        /// <value>
        /// Only get the primary table from GameJolt Game API
        /// </value>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public ScoreTable Primary
        {
            get
            {
                XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                ScoreTable result = null;
                foreach (XElement table in response.Element("tables").Elements("table"))
                {
                    if (table.Element("primary").Value == "1")
                    {
                        result = new ScoreTable(int.Parse(table.Element("id").Value), WebCaller);
                        break;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Initialize table manager and download a test purpose list
        /// </summary>
        /// <param name="webCaller">A instance of <see cref="WebCaller"/> to download the data</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public TableManager(WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

        /// <summary>
        /// Get the table name using the <paramref name="tableid"/>
        /// </summary>
        /// <param name="tableid">Table id used as reference</param>
        /// <returns>The name of table or null if doesnt exists</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public string GetName(int tableid)
        {
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            string result = null;
            foreach (XElement table in response.Element("tables").Elements("table"))
            {
                if (table.Element("id").Value == tableid.ToString())
                {
                    result = table.Element("name").Value;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Get the table description using the <paramref name="tableid"/>
        /// </summary>
        /// <param name="tableid">Table id used as reference</param>
        /// <returns>The name of table or null if doesnt exists</returns>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        public string GetDescription(int tableid)
        {
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            string result = null;
            foreach (XElement table in response.Element("tables").Elements("table"))
            {
                if (table.Element("id").Value == tableid.ToString())
                {
                    result = table.Element("description").Value;
                    break;
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public WebCaller WebCaller { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}
