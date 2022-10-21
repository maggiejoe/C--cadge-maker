using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;

namespace CatWorx.BadgeMaker
{
    class Util
    {
        // Method declared as "static"
        public static void PrintEmployees(List<Employee> employees)
        {
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                file.WriteLine("Id, Name, PhotoUrl");
            

                for (int i = 0; i < employees.Count; i++)
                {
                    // Each item in employees is now an Employee instance
                    string template = "{0},{1},{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
                }
            }
        }

        public static void MakeCSV(List<Employee> employees)
        {
            // Check to see if the data directory exists
            if (!Directory.Exists("data"))
            {
                // If it doesn't, create it
                Directory.CreateDirectory("data");
            }

        }

            public static void MakeBadges(List<Employee> employees)
            {
                // ** REMOVING CODE FOR MODIFICATIONS BUT WANT TO KEEP FOR REFERENCE **
                // Create image
                // SKImage newImage = SKImage.FromEncodedData(File.OpenRead("badge.png"));

                // SKData data = newImage.Encode();
                // data.SaveTo(File.OpenWrite("data/empployeeBadge.png"));

                
            }
    }
}