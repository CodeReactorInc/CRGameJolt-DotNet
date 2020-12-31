using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{

    public class GameLogin
    {
        public string GameId { get; private set; }
        public string GameKey { get; private set; }

        public void Collect()
        {
            Console.Write("Game ID: ");
            GameId = Console.ReadLine();
            Console.Write("Game Key: ");
            GameKey = Console.ReadLine();
        }
    }
}
