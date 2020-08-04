using System;
using System.Collections.Generic;

namespace CalvinExampleStuff
{
    class Company
    {
        public string CompanyName { get; set; }
        public List<Employee> Employees { get; set; }

        public Company(string companyName)
        {
            CompanyName = companyName;
            Employees = new List<Employee>();
        }

        public string GetNameForEmail()
        {
            return CompanyName.Replace(".", "").Replace(" ", "");
        }

        public void PrintBanner()
        {
            Console.WriteLine($" -###- {CompanyName} -###-");
        }

        public void FireEmployes()
        {
            Console.Write("First Name of Person to fire:");
            string firstName = Console.ReadLine();
            List<Employee> employeesToFire = Employees.FindAll(employee => employee.FirstName == firstName);

            if(employeesToFire.Count == 0)
            {
                Console.WriteLine("No employees match search.");
                return;
            }
            foreach (var employee in Employees)
            {
                Console.WriteLine($"Do you want to fire {employee.FirstName} {employee.LastName}? Press 'y' to fire or any key to continue.");
                var key = Console.ReadKey(true).KeyChar.ToString();
                if (key == "y")
                {
                    Employees.Remove(employee);
                }
            }
            Console.WriteLine("No more employees left in search.");
        }
    }
}