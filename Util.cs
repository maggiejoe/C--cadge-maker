using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;
using System.Threading.Tasks;

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

        async public static Task MakeBadges(List<Employee> employees)
        {
            // Layout variables in pixels
            // Full badge sizes
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;

            // Image sizes
            int PHOTO_LEFT_X = 184;
            int PHOTO_TOP_Y = 215;
            int PHOTO_RIGHT_X = 486;
            int PHOTO_BOTTOM_Y = 517;

            // company name sizes
            int COMPANY_NAME_Y = 150;

            // employe info sizes
            int EMPLOYEE_NAME_Y = 600;
            int EMPLOYEE_ID_Y = 730;

            // Values needed for adding the company name text to the badge
            SKPaint paint = new SKPaint();
            paint.TextSize = 42.0f;
            paint.IsAntialias = true;
            paint.Color = SKColors.White;
            paint.IsStroke = false;
            paint.TextAlign = SKTextAlign.Center;
            paint.Typeface = SKTypeface.FromFamilyName("Arial");

            // instance of HttpClient is disposed after code in the block has run
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));
                    SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

                    SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);
                    SKCanvas canvas = new SKCanvas(badge);

                    // DrawImage() method's arguments involve taking in an SKImage object and using the SKRect (SKRectangle) object for placement and size
                    // First two arguments in SKRect indicate the coordinates for the upper-left corner of the rectangle on an x&y axis beginning with the x axis
                    // Following two arguments indicate the coordinates for the lower right corner of the rectangle on the x & y axix beginning with the x axis
                    canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));

                    // allows us to insert imployee photo onto the SKCanvas object with specific coordinates and size dimensions
                    canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));

                    SKImage finalImage = SKImage.FromBitmap(badge);
                    SKData data = finalImage.Encode();
                    
                    // save badge file as a different file for each employee
                    string template = "data/{0}_badge.png";
                    data.SaveTo(File.OpenWrite(string.Format(template, employees[i].GetId())));

                    // Company name & Employee Name
                    canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);
                    canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint);
                    
                    paint.Typeface = SKTypeface.FromFamilyName("Courier New");
                    // Employee Id
                    canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint);
                }
            }
        }
    }
}