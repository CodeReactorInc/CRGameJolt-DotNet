using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class LoginMenu
    {
        public static void Collect()
        {
            if (MainMenu.Instance.Memory.GameJolt.UserLogged != null)
            {
                Console.WriteLine("You need restart to new login info be loaded");
            }

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("User Token: ");
            string token = Console.ReadLine();
            Console.WriteLine("Trying login after save...");
            try
            {
                MainMenu.Instance.Memory.GameJolt.Login(username, token);
                MainMenu.Instance.Memory.Options.Username = username;
                MainMenu.Instance.Memory.Options.UserToken = token;
                MainMenu.Instance.Memory.Options.Save();
                MainMenu.Instance.Start();
            } catch (GameJoltAPIException)
            {
                Console.WriteLine("Invalid user information");
                MainMenu.Instance.Start();
            } catch (Exception e)
            {
                Console.WriteLine("A Exception has throwed: " + e.ToString());
                Environment.Exit(1);
                return;
            }
        }
    }
}
