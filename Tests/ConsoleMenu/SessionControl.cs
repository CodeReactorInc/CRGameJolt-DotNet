using CodeReactor.CRGameJolt.Users;
using System;
using System.Threading;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class SessionControl
    {
        public static void Collect()
        {
            Console.WriteLine("Getting or creating a instance for SessionManager");
            SessionManager manager = MainMenu.Instance.Memory.GameJolt.UserLogged.Session;
            Console.WriteLine("Checking atual state...");
            if (manager.Check())
            {
                Console.WriteLine("1. Set active");
                Console.WriteLine("2. Set idle");
                Console.WriteLine("3. Enable/Disable Autoping");
                Console.WriteLine("4. Close");
                Console.WriteLine("5. Ping");
                Console.WriteLine("6. Back");
                Console.Write("Select one option: ");
                try
                {
                    int o = int.Parse(Console.ReadLine());

                    switch (o)
                    {
                        case 1:
                            manager.Status = SessionStatus.Active;
                            Console.WriteLine("Status configured");
                            Collect();
                            break;
                        case 2:
                            manager.Status = SessionStatus.Idle;
                            Console.WriteLine("Status configured");
                            Collect();
                            break;
                        case 3:
                            if (MainMenu.Instance.Memory.Autoping)
                            {
                                MainMenu.Instance.Memory.Autoping = false;
                                if (MainMenu.Instance.Memory.AutopingThread != null)
                                {
                                    try
                                    {
                                        MainMenu.Instance.Memory.AutopingThread.Abort();
                                    }
                                    catch (PlatformNotSupportedException) { }
                                }
                                MainMenu.Instance.Memory.AutopingThread = null;
                                Console.WriteLine("Autoping disabled");
                                Collect();
                            }
                            else
                            {
                                MainMenu.Instance.Memory.Autoping = true;
                                MainMenu.Instance.Memory.AutopingThread = new Thread(Autoping);
                                MainMenu.Instance.Memory.AutopingThread.Name = "Autoping Thread";
                                MainMenu.Instance.Memory.AutopingThread.Start(MainMenu.Instance.Memory);
                                Console.WriteLine("Autoping enabled");
                                Collect();
                            }
                            break;
                        case 4:
                            Console.WriteLine("Closing session...");
                            manager.Close();
                            Collect();
                            break;
                        case 5:
                            Console.WriteLine("Manual ping...");
                            manager.Ping();
                            Collect();
                            break;
                        case 6:
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
            else
            {
                Console.WriteLine("1. Open session");
                Console.WriteLine("2. Back");
                Console.Write("Select one option: ");
                try
                {
                    int o = int.Parse(Console.ReadLine());

                    switch (o)
                    {
                        case 1:
                            manager.Open();
                            Console.WriteLine("Opened session");
                            Collect();
                            break;
                        case 2:
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
        }

        public static void Autoping(object data)
        {
            CentralMemory memory = (CentralMemory)data;
            while (memory.Autoping)
            {
                memory.GameJolt.UserLogged.Session.Ping();
                Thread.Sleep((int)SessionManager.Timeout);
            }
        }
    }
}
