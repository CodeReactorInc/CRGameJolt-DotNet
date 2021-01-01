using CodeReactor.CRGameJolt.Users;
using System;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class FriendProcessor
    {
        public static void Collect()
        {
            Console.WriteLine("Updating or creating a instance of FriendList...");

            MainMenu.Instance.Memory.GameJolt.UserLogged.Friends.Update();

            foreach (GameJoltUser user in MainMenu.Instance.Memory.GameJolt.UserLogged.Friends.Values)
            {
                UserInfo.ShowInfo(user);
            }

            MainMenu.Instance.Start();
        }
    }
}
