using System;
using System.Collections.Generic;

namespace CalvinExampleStuff
{
    internal class Company
    {
        private string CompanyName { get; }
        public List<Employee> Employees { get; }

        public Company(string companyName)
        {
            CompanyName = companyName;
            Employees = new List<Employee>();
        }

        public void Something()
        {
            var list = Employees.FindAll(x => x is TemporaryEmployee);
        }

        public string GetNameForEmail()
        {
            return CompanyName.Replace(".", "").Replace(" ", "");
        }

        public void PrintBanner()
        {
            Console.WriteLine($" -###- {CompanyName} -###-");
        }

        public void FireEmployees()
        {
            Console.Write("First Name of Person to fire:");
            var firstName = Console.ReadLine();
            var employeesToFire = Employees.FindAll(employee => employee.FirstName == firstName);

            if (employeesToFire.Count == 0)
            {
                Console.WriteLine("No employees match search.");
                return;
            }

            foreach (var employee in Employees)
            {
                Console.WriteLine(
                    $"Do you want to fire {employee.FirstName} {employee.LastName}? Press 'y' to fire or any key to continue.");
                var key = Console.ReadKey(true).KeyChar.ToString();
                if (key == "y") Employees.Remove(employee);
            }

            Console.WriteLine("No more employees left in search.");
        }
    }
}