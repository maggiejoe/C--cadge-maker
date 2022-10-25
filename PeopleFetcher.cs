using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        // Collects employee names as strings and adds them to a list
        async public static Task<List<Employee>> GetEmployees()
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

        async public static Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);

                foreach (JToken token in json.SelectToken("results")!)
                {
                    // Parse JSON data
                    Employee emp = new Employee
                    (
                      token.SelectToken("name.first")!.ToString(),
                      token.SelectToken("name.last")!.ToString(),
                      Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
                      token.SelectToken("picture.large")!.ToString()
                    );
                    employees.Add(emp);
                }
            }

            return employees;
        }

    }
}