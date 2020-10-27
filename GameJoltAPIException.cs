using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt
{
    /// <summary>
    /// Contains a error from GameJolt Game API
    /// </summary>
    public class GameJoltAPIException : Exception
    {
        public GameJoltAPIException() : base() { }
        public GameJoltAPIException(string message) : base(message) { }
        public GameJoltAPIException(string message, Exception inner) : base(message, inner) { }
    }
}
