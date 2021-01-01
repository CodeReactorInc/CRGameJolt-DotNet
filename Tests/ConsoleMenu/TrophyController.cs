using CodeReactor.CRGameJolt.Users.Trophies;
using System;
using System.Collections.Generic;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class TrophyController
    {
        public static void Collect()
        {
            Console.WriteLine("Updating or creating a instance of TrophiesManager...");

            TrophiesManager manager = MainMenu.Instance.Memory.GameJolt.UserLogged.Trophies;
            manager.Update();

            Console.WriteLine("1. Add trophy");
            Console.WriteLine("2. Remove trophy");
            Console.WriteLine("3. See all trophy");
            Console.WriteLine("4. Back");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        try
                        {
                            Console.Write("Trophy ID: ");
                            int id = int.Parse(Console.ReadLine());
                            try
                            {
                                manager[id].Add();
                                Console.WriteLine("Added trophy: " + id);
                            }
                            catch (KeyNotFoundException)
                            {
                                Console.WriteLine("Trophy doesn't found");
                            }
                            catch (GameJoltAPIException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect();
                        break;
                    case 2:
                        try
                        {
                            Console.Write("Trophy ID: ");
                            int id = int.Parse(Console.ReadLine());
                            try
                            {
                                manager[id].Remove();
                                Console.WriteLine("Removed trophy: " + id);
                            }
                            catch (KeyNotFoundException)
                            {
                                Console.WriteLine("Trophy doesn't found");
                            }
                            catch (GameJoltAPIException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect();
                        break;
                        break;
                    case 3:
                        foreach (Trophy trophy in manager.Values)
                        {
                            ShowTrophy(trophy);
                        }
                        Collect();
                        break;
                    case 4:
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

        public static void ShowTrophy(Trophy trophy)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Title: " + trophy.Title);
            Console.WriteLine("Description: " + trophy.Description);
            Console.WriteLine("ID: " + trophy.Id);
            Console.WriteLine("Image URL: " + trophy.ImageURL);
            Console.WriteLine("Achieved: " + (trophy.Achieved ? "yes" : "false"));
            Console.WriteLine("Difficulty: " + GetDifficulty(trophy.Difficulty));
        }

        public static string GetDifficulty(TrophyDifficulty difficulty)
        {
            switch (difficulty)
            {
                case TrophyDifficulty.Bronze:
                    return "Bronze";
                case TrophyDifficulty.Silver:
                    return "Silver";
                case TrophyDifficulty.Gold:
                    return "Gold";
                case TrophyDifficulty.Platinum:
                    return "Platinum";
                default:
                    return "Error";
            }
        }
    }
}
