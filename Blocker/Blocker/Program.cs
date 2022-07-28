using System;

namespace Blocker
{
    class Program
    {
        static void Start(string[] args)
        {
            string command = args[0].Remove(0, 1).ToUpper();

            switch (command)
            {
                case "HELP":
                    {
                        Console.WriteLine("Usage: <command> <params> - execute command");
                        Console.WriteLine();
                        Console.WriteLine("Command list: ");
                        Console.WriteLine("/help - show information");
                        Console.WriteLine("/add <url> - add new host");
                        Console.WriteLine("/rem <url> - remove host");
                        break;
                    }
                case "ADD":
                    {
                        try
                        {
                            Host.Add(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("The <url> argument is required!");
                        }
                        break;
                    }
                case "REM":
                    {
                        try
                        {
                            Host.Rem(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("The <url> argument is required!");
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command: " + args[0].ToLower());
                        break;
                    }
            }
        }

        static void Main(string[] args)
        {
            Utils.Install();
            Host.Initialization();

            try
            {
                Utils.Copyright(Host.file);

                while (true)
                {
                    Console.Write("blocker> ");
                    string command = Console.ReadLine();

                    if (!string.IsNullOrEmpty(command))
                    {
                        Start(command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex.Message);
            }

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
