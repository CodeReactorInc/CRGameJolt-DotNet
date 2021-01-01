using System;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public class MainMenu
    {
        public static MainMenu Instance { get; private set; }

        public CentralMemory Memory { get; set; }

        public MainMenu(CentralMemory memory)
        {
            Instance = this;
            Memory = memory;
        }

        public void Start()
        {
            Console.WriteLine("1. User info");
            Console.WriteLine("2. Session Control");
            Console.WriteLine("3. Score/Table");
            Console.WriteLine("4. Trophy");
            Console.WriteLine("5. User Data Storage");
            Console.WriteLine("6. Global Data Storage");
            Console.WriteLine("7. Friends");
            Console.WriteLine("8. Time");
            Console.WriteLine("9. Login");
            Console.WriteLine("10. Exit");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        UserInfo.Collect();
                        break;
                    case 2:
                        if (Memory.GameJolt.UserLogged == null)
                        {
                            Console.WriteLine("Please, make login");
                            Start();
                            return;
                        }
                        SessionControl.Collect();
                        break;
                    case 3:
                        TableSelector.Collect();
                        break;
                    case 4:
                        if (Memory.GameJolt.UserLogged == null)
                        {
                            Console.WriteLine("Please, make login");
                            Start();
                            return;
                        }
                        TrophyController.Collect();
                        break;
                    case 5:
                        if (Memory.GameJolt.UserLogged == null)
                        {
                            Console.WriteLine("Please, make login");
                            Start();
                            return;
                        }
                        DataStorageMenu.Collect(Memory.GameJolt.UserDataStorage);
                        break;
                    case 6:
                        DataStorageMenu.Collect(Memory.GameJolt.GlobalDataStorage);
                        break;
                    case 7:
                        FriendProcessor.Collect();
                        break;
                    case 8:
                        Console.WriteLine("Requesting time for GameJolt...");
                        DateTime now = Memory.GameJolt.Time;
                        Console.WriteLine(now.ToString("G"));
                        Start();
                        break;
                    case 9:
                        LoginMenu.Collect();
                        break;
                    case 10:
                        Console.WriteLine("Thanks for using Code Reactor technology!");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Invalid number, try again");
                        Start();
                        break;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number, try again");
                Start();
            }
        }
    }
}
