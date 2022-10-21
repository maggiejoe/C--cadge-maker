using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // Collects employee names as strings and adds them to a list
        static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            while (true)
            {

                // Collect first name
                Console.WriteLine("Please enter first name (leave empty to exit): ");
                string firstName = Console.ReadLine() ?? "";

                // if statement that prevents while loop from running indefinitely
                if (firstName == "")
                {
                    break;
                }

                // Collect last name
                Console.WriteLine("Please enter last name: ");
                string lastName = Console.ReadLine() ?? "";

                // Collect id
                Console.WriteLine("Please enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");

                // Collect photo url
                Console.WriteLine("Please enter photo URL: ");
                string photoUrl = Console.ReadLine() ?? "";
                
                // Create a new Employee instance
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }
            return employees;
        }


        // calls GetEmployees() and PrintEmployees() methods
        async static Task Main(string[] args)
        {
            List<Employee> employees = GetEmployees();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
        }
    }
}