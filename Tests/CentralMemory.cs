using CodeReactor.CRGameJolt.Test.Configuration;
using CodeReactor.CRGameJolt.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CodeReactor.CRGameJolt.Test
{
    public class CentralMemory
    {
        public GameJolt GameJolt { get; set; }
        public XmlConfiguration Options { get; set; }
        public Thread AutopingThread { get; set; }
        public bool Autoping { get; set; }
    }
}
