using CodeReactor.CRGameJolt.Test.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using CodeReactor.CRGameJolt.Test.ConsoleMenu;
using CodeReactor.CRGameJolt.DataStorage;
using System.Net;

namespace CodeReactor.CRGameJolt.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting a instance of CentralMemory...");
            CentralMemory memory = new CentralMemory();
            Console.WriteLine("Trying to read Options.xml...");

            try
            {
                memory.Options = new XmlConfiguration(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "/Options.xml");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found, creating one...");
                GameLogin glogin = new GameLogin();
                glogin.Collect();
                try
                {
                    memory.Options = new XmlConfiguration(glogin.GameId, glogin.GameKey, Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "/Options.xml");
                }
                catch (Exception e)
                {
                    Console.WriteLine("A Exception has throwed: " + e.ToString());
                    Environment.Exit(1);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("A Exception has throwed: " + e.ToString());
                Environment.Exit(1);
                return;
            }

            Console.WriteLine("Creating a instance of GameJolt...");
            memory.GameJolt = new GameJolt(memory.Options.GameId, memory.Options.GameKey);
            Console.WriteLine("Setting WebCaller.log as Debugger on WebCaller...");
            memory.GameJolt.WebCaller.Debug = new StreamWriter(new FileStream(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "/WebCaller.log", FileMode.Create));
            Console.WriteLine("Trying to send a test payload to Global Data Storage...");
            try { 
                memory.GameJolt.GlobalDataStorage["last_test"] = new DataStorageValue(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
            }
            catch (WebException)
            {
                Console.WriteLine("Please check your internet connection and try later...");
            }
            catch (Exception e)
            {
                Console.WriteLine("A Exception has throwed: " + e.ToString());
                Environment.Exit(1);
                return;
            }

            Console.WriteLine("Trying to login in...");
            try
            {
                memory.GameJolt.Login(memory.Options.Username, memory.Options.UserToken);
            }
            catch (GameJoltAPIException)
            {
                Console.WriteLine("Login information invalid, please change on main menu");
            }
            catch (WebException)
            {
                Console.WriteLine("Please check your internet connection and try later...");
            }
            catch (Exception e)
            {
                Console.WriteLine("A Exception has throwed: " + e.ToString());
                Environment.Exit(1);
                return;
            }

            Console.WriteLine("Saving Options.xml and showing main menu...");
            try
            {
                memory.Options.Save();
            } catch (Exception e)
            {
                Console.WriteLine("A Exception has throwed: " + e.ToString());
                Environment.Exit(1);
                return;
            }

            MainMenu mainMenu = new MainMenu(memory);
            mainMenu.Start();
        }
    }
}
