using CodeReactor.CRGameJolt.DataStorage;
using System;

namespace CodeReactor.CRGameJolt.Test.ConsoleMenu
{
    public static class DataStorageMenu
    {
        public static void Collect(IGJDataStorage ds)
        {
            Console.WriteLine("1. Set");
            Console.WriteLine("2. Get");
            Console.WriteLine("3. Remove");
            Console.WriteLine("4. Add");
            Console.WriteLine("5. Subtract");
            Console.WriteLine("6. Multiply");
            Console.WriteLine("7. Divide");
            Console.WriteLine("8. Append");
            Console.WriteLine("9. Prepend");
            Console.WriteLine("10. Keys");
            Console.WriteLine("11. Back");
            Console.Write("Select one option: ");
            try
            {
                int o = int.Parse(Console.ReadLine());

                switch (o)
                {
                    case 1:
                        Console.Write("Key: ");
                        string key = Console.ReadLine();
                        Console.Write("Value: ");
                        int valuei = 0;
                        string values = Console.ReadLine();
                        if (int.TryParse(values, out valuei))
                        {
                            ds[key] = new DataStorageValue(valuei);
                        }
                        else
                        {
                            ds[key] = new DataStorageValue(values);
                        }
                        Console.WriteLine("Value setted");
                        Collect(ds);
                        break;
                    case 2:
                        Console.Write("Key: ");
                        string key0 = Console.ReadLine();
                        try
                        {
                            DataStorageValue dsv = ds[key0];
                            if (dsv.Type == DSValueType.STRING)
                            {
                                Console.WriteLine("Value: " + dsv);
                            }
                            else
                            {
                                Console.WriteLine("Value: " + ((int)dsv).ToString());
                            }
                        }
                        catch (GameJoltAPIException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Collect(ds);
                        break;
                    case 3:
                        Console.Write("Key: ");
                        string key1 = Console.ReadLine();
                        try
                        {
                            ds.Remove(key1);
                            Console.WriteLine("Value removed");
                        }
                        catch (GameJoltAPIException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Collect(ds);
                        break;
                    case 4:
                        try
                        {
                            Console.Write("Key: ");
                            string key2 = Console.ReadLine();
                            Console.Write("Value: ");
                            int valuei0 = int.Parse(Console.ReadLine());
                            try
                            {
                                ds.Add(key2, valuei0);
                                Console.WriteLine("Value added");
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
                        Collect(ds);
                        break;
                    case 5:
                        try
                        {
                            Console.Write("Key: ");
                            string key2 = Console.ReadLine();
                            Console.Write("Value: ");
                            int valuei0 = int.Parse(Console.ReadLine());
                            try
                            {
                                ds.Subtract(key2, valuei0);
                                Console.WriteLine("Value subtract");
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
                        Collect(ds);
                        break;
                    case 6:
                        try
                        {
                            Console.Write("Key: ");
                            string key2 = Console.ReadLine();
                            Console.Write("Value: ");
                            int valuei0 = int.Parse(Console.ReadLine());
                            try
                            {
                                ds.Multiply(key2, valuei0);
                                Console.WriteLine("Value multiply");
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
                        Collect(ds);
                        break;
                    case 7:
                        try
                        {
                            Console.Write("Key: ");
                            string key2 = Console.ReadLine();
                            Console.Write("Value: ");
                            int valuei0 = int.Parse(Console.ReadLine());
                            try
                            {
                                ds.Divide(key2, valuei0);
                                Console.WriteLine("Value divide");
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
                        Collect(ds);
                        break;
                    case 8:
                        Console.Write("Key: ");
                        string key6 = Console.ReadLine();
                        Console.Write("Value: ");
                        string value0 = Console.ReadLine();
                        try
                        {
                            ds.Append(key6, value0);
                            Console.WriteLine("Value appended");
                        }
                        catch (GameJoltAPIException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Collect(ds);
                        break;
                    case 9:
                        Console.Write("Key: ");
                        string key7 = Console.ReadLine();
                        Console.Write("Value: ");
                        string value1 = Console.ReadLine();
                        try
                        {
                            ds.Append(key7, value1);
                            Console.WriteLine("Value prepended");
                        }
                        catch (GameJoltAPIException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Collect(ds);
                        break;
                    case 10:
                        Console.WriteLine("All keys:");
                        foreach (string key8 in ds.Keys)
                        {
                            Console.WriteLine(key8);
                        }
                        Collect(ds);
                        return;
                    case 11:
                        MainMenu.Instance.Start();
                        return;
                    default:
                        Console.WriteLine("Invalid number, try again");
                        Collect(ds);
                        break;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number, try again");
                Collect(ds);
            }
        }
    }
}
