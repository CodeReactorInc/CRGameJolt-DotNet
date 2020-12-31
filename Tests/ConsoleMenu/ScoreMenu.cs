using CodeReactor.CRGameJolt.Scores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public class ScoreMenu
    {
        public static void Collect(ScoreTable table)
        {
            Console.WriteLine("1. Add user");
            Console.WriteLine("2. Add guest");
            Console.WriteLine("3. Fetch user");
            Console.WriteLine("4. Fetch guest");
            Console.WriteLine("5. Fetch all");
            Console.WriteLine("6. Better Than");
            Console.WriteLine("7. Worse Than");
            Console.WriteLine("8. Get rank");
            Console.WriteLine("9. Back");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        if (MainMenu.Instance.Memory.GameJolt.UserLogged == null)
                        {
                            Console.WriteLine("User not logged");
                            Collect(table);
                            return;
                        }
                        try
                        {
                            Console.Write("Score: ");
                            string score = Console.ReadLine();
                            Console.Write("Sort: ");
                            int sort = int.Parse(Console.ReadLine());
                            Console.Write("Extra data: ");
                            string extradata = Console.ReadLine();
                            table.Add(score, sort, extradata, MainMenu.Instance.Memory.GameJolt.UserLogged);
                            Console.WriteLine("Score added");
                            Collect(table);
                        } catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                            Collect(table);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.Write("Score: ");
                            string score = Console.ReadLine();
                            Console.Write("Sort: ");
                            int sort = int.Parse(Console.ReadLine());
                            Console.Write("Extra data: ");
                            string extradata = Console.ReadLine();
                            Console.Write("Guest: ");
                            string guest0 = Console.ReadLine();
                            table.Add(score, sort, extradata, guest0);
                            Console.WriteLine("Score added");
                            Collect(table);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                            Collect(table);
                        }
                        break;
                    case 3:
                        if (MainMenu.Instance.Memory.GameJolt.UserLogged == null)
                        {
                            Console.WriteLine("User not logged");
                            Collect(table);
                            return;
                        }
                        ShowScore(table.Fetch(MainMenu.Instance.Memory.GameJolt.UserLogged));
                        Collect(table);
                        break;
                    case 4:
                        Console.Write("Guest: ");
                        string guest = Console.ReadLine();
                        try
                        {
                            ShowScore(table.Fetch(guest));
                        } catch (GameJoltAPIException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Collect(table);
                        break;
                    case 5:
                        try
                        {
                            Console.Write("Limit: ");
                            int limit = int.Parse(Console.ReadLine());
                            try
                            {
                                foreach(ScoreValue value in table.Fetch(limit))
                                {
                                    ShowScore(value);
                                }
                            }
                            catch (GameJoltAPIException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Invalid limit provided");
                            }
                        } catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect(table);
                        break;
                    case 6:
                        try
                        {
                            Console.Write("Sort: ");
                            int sort = int.Parse(Console.ReadLine());
                            Console.Write("Limit: ");
                            int limit = int.Parse(Console.ReadLine());
                            try
                            {
                                foreach (ScoreValue value in table.BetterThan(sort, limit))
                                {
                                    ShowScore(value);
                                }
                            }
                            catch (GameJoltAPIException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Invalid limit provided");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect(table);
                        break;
                    case 7:
                        try
                        {
                            Console.Write("Sort: ");
                            int sort = int.Parse(Console.ReadLine());
                            Console.Write("Limit: ");
                            int limit = int.Parse(Console.ReadLine());
                            try
                            {
                                foreach (ScoreValue value in table.WorseThan(sort, limit))
                                {
                                    ShowScore(value);
                                }
                            }
                            catch (GameJoltAPIException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Invalid limit provided");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect(table);
                        break;
                    case 8:
                        try
                        {
                            Console.Write("Sort: ");
                            int sort = int.Parse(Console.ReadLine());
                            Console.WriteLine("Rank: " + table.GetRank(sort));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                        }
                        Collect(table);
                        break;
                    case 9:
                        MainMenu.Instance.Start();
                        break;
                    default:
                        Console.WriteLine("Invalid number, try again");
                        Collect(table);
                        break;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number, try again");
                Collect(table);
            }
        }

        public static void ShowScore(ScoreValue score)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Score: " + score.Score);
            Console.WriteLine("Sort: " + score.Sort);
            Console.WriteLine("Extra Data: " + score.ExtraData);
            if (score.Guest != null)
            {
                Console.WriteLine("Guest: " + score.Guest);
            } else
            {

                Console.WriteLine("Username: " + score.User);
                Console.WriteLine("User ID: " + score.UserId);
            }
            Console.WriteLine("Stored: " + score.Stored.ToString("G"));
        }
    }
}
