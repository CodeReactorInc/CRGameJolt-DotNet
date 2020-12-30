using CodeReactor.CRGameJolt.Connector;
using CodeReactor.CRGameJolt.Users;

namespace CodeReactor.CRGameJolt.Scores
{
    public class ScoreTable : IGJObject
    {
        public int Id { get; private set; }

        public ScoreTable(int tableid, WebCaller webcaller) { }

        public ScoreValue Fetch(GameJoltMe user) { }

        public ScoreValue Fetch(string guest) { }

        public ScoreValue[] Fetch(int limit) { }

        public ScoreValue[] BetterThan(int sort, int limit) { }

        public ScoreValue[] WorseThan(int sort, int limit) { }

        public void Add(string score, int sort, string extradata, GameJoltMe user) { }

        public void Add(string score, int sort, string extradata, string guest) { }

        public int GetRank(int sort) { }

        public WebCaller WebCaller { get; set; }

        public void Update()
        {
            
        }
    }
}
