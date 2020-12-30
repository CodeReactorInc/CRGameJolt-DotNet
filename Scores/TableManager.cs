using CodeReactor.CRGameJolt.Connector;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Scores
{
    public class TableManager : IGJObject
    {
        public ScoreTable this[int tableid]
        {
            get
            {
                XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
                if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
                ScoreTable result = null;
                foreach(XElement table in response.Element("tables").Elements("table"))
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

        public TableManager(WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }

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

        public WebCaller WebCaller { get; set; }

        public void Update()
        {
            XElement response = WebCaller.GetAsXML("scores/tables", new string[] { }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}
