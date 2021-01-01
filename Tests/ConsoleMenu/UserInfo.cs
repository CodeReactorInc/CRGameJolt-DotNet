using CodeReactor.CRGameJolt.Users;
using System;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class UserInfo
    {
        public static void Collect()
        {
            Console.WriteLine("1. Username");
            Console.WriteLine("2. User ID");
            Console.WriteLine("3. Back");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        Console.Write("Username: ");
                        try
                        {
                            string username = Console.ReadLine();
                            Console.WriteLine("Fetching user info...");
                            ShowInfo(MainMenu.Instance.Memory.GameJolt.FetchUser(username));
                            MainMenu.Instance.Start();
                        }
                        catch (GameJoltAPIException)
                        {
                            Console.WriteLine("User not found");
                            MainMenu.Instance.Start();
                        }
                        break;
                    case 2:
                        Console.Write("User ID: ");
                        try
                        {
                            int userid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Fetching user info...");
                            ShowInfo(MainMenu.Instance.Memory.GameJolt.FetchUser(userid));
                            MainMenu.Instance.Start();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                            Collect();
                        }
                        catch (GameJoltAPIException)
                        {
                            Console.WriteLine("User not found");
                            MainMenu.Instance.Start();
                        }
                        break;
                    case 3:
                        MainMenu.Instance.Start();
                        break;
                    default:
                        Console.WriteLine("Invalid number, try again");
                        Collect();
                        break;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number, try again");
                Collect();
            }
        }

        public static void ShowInfo(GameJoltUser user)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("User ID: " + user.Id);
            Console.WriteLine("Avatar URL: " + user.AvatarURL);
            Console.WriteLine("Last Logged In: " + user.LastLoggedIn.ToString("G"));
            Console.WriteLine("Signed Up: " + user.SignedUp.ToString("G"));
            Console.WriteLine("Status: " + GetStatus(user.Status));
            Console.WriteLine("Type: " + GetType(user.Type));
            Console.WriteLine("Developer Name: " + user.DeveloperName);
            Console.WriteLine("Developer Description: " + user.DeveloperDescription);
            Console.WriteLine("Developer Website: " + user.DeveloperWebsite);
        }

        public static string GetStatus(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.Active:
                    return "Active";
                case UserStatus.Banned:
                    return "Banned";
                default:
                    return "Error!";
            }
        }

        public static string GetType(UserType type)
        {
            switch (type)
            {
                case UserType.Administrator:
                    return "Administrator";
                case UserType.Developer:
                    return "Developer";
                case UserType.Moderator:
                    return "Moderator";
                case UserType.User:
                    return "User";
                default:
                    return "Error!";
            }
        }
    }
}
