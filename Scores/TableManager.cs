using CodeReactor.CRGameJolt.Connector;

namespace CodeReactor.CRGameJolt.Scores
{
    public class TableManager : IGJObject
    {
        public ScoreTable this[int tableid]
        {
            get
            {

            }
        }

        public ScoreTable[] Tables
        {
            get
            {

            }
        }

        public ScoreTable Primary
        {
            get
            {

            }
        }

        public TableManager(WebCaller webCaller)
        {

        }

        public string GetName(int tableid)
        {

        }

        public string GetDescription(int tableid)
        {

        }

        public WebCaller WebCaller { get; set; }

        public void Update()
        {

        }
    }
}
