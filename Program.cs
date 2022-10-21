using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // calls GetEmployees(), PrintEmployees(), MakeCSV() and MakeBadges() methods
        async static Task Main(string[] args)
        {
            // List<Employee> employees = PeopleFetcher.GetEmployees();
            List<Employee> employees = await PeopleFetcher.GetFromApi();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
        }
    }
}