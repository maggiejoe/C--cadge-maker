using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // calls GetEmployees(), PrintEmployees(), MakeCSV() and MakeBadges() methods
        async static Task Main(string[] args)
        {
            bool confirm = false;

            Console.WriteLine("Hey, welcome to catWorx!");

            ConsoleKey response;

            do
            {
                Console.WriteLine("Would you like to use the API to randomly generate Employees? [y/n]");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            confirm = response == ConsoleKey.Y;
            if (!confirm)
            {
                List<Employee> employees = await PeopleFetcher.GetEmployees();
                Util.PrintEmployees(employees);
                Util.MakeCSV(employees);
                await Util.MakeBadges(employees);
            }
            else
            {
                List<Employee> employees = await PeopleFetcher.GetFromApi();
                Util.PrintEmployees(employees);
                Util.MakeCSV(employees);
                await Util.MakeBadges(employees);
            }
        }
    }
}