using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Scores
{
    public class ScoreTable : IGJObject
    {
        public int Id { get; private set; }

        public ScoreTable(int tableid, WebCaller webCaller)
        {
            WebCaller = webCaller;
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=1", "table_id=" + tableid }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            Id = tableid;
        }

        public ScoreValue Fetch(GameJoltMe user)
        {
            return new ScoreValue(this, user, WebCaller);
        }

        public ScoreValue Fetch(string guest)
        {
            return new ScoreValue(this, guest, WebCaller);
        }

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

        public int GetRank(int sort)
        {
            XElement response = WebCaller.GetAsXML("scores/get-rank", new string[] { "table_id=" + Id, "sort=" + sort }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
            return int.Parse(response.Element("rank").Value);
        }

        public WebCaller WebCaller { get; set; }

        public void Update()
        {
            XElement response = WebCaller.GetAsXML("scores", new string[] { "limit=1", "table_id=" + Id }).Element("response");
            if (response.Element("success").Value != "true") throw new GameJoltAPIException(response.Element("message").Value);
        }
    }
}
