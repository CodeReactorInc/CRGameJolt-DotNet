using CodeReactor.CRGameJolt.Scores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class TableSelector
    {
        public static void Collect()
        {
            TableManager manager = MainMenu.Instance.Memory.GameJolt.Tables;
            Console.WriteLine("1. View tables");
            Console.WriteLine("2. Enter table");
            Console.WriteLine("3. Back");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        foreach(ScoreTable table in manager.Tables)
                        {
                            Console.WriteLine("------------------------------");
                            Console.WriteLine("Table ID: " + table.Id);
                            Console.WriteLine("Table Name: " + manager.GetName(table.Id));
                            Console.WriteLine("Table Description: " + manager.GetDescription(table.Id));
                            Console.WriteLine("Primary: " + ((manager.Primary.Id == table.Id) ? "yes" : "no"));
                        }
                        Collect();
                        break;
                    case 2:
                        Console.Write("Table ID: ");
                        try
                        {
                            int tableid = int.Parse(Console.ReadLine());
                            ScoreTable table = manager[tableid];
                            if (table == null)
                            {
                                Console.WriteLine("Can't find the table, try again");
                                Collect();
                                return;
                            }
                            ScoreMenu.Collect(table);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid number, try again");
                            Collect();
                        } catch (GameJoltAPIException)
                        {
                            Console.WriteLine("Can't find the table, try again");
                            Collect();
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
    }
}
