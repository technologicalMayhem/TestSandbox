using ConsoleAppUtil;
using System;

namespace CalvinExampleStuff
{
    internal static class Program
    {
        private static Company Company { get; set; }
        private static bool ShuttingDown { get; set; }

        private static void Main()
        {
            Console.Write("Give your company a name: ");
            var companyName = Console.ReadLine();
            Company = new Company(companyName);
            GetDefaultEmployees();
            var menu = new Menu(new[]
            {
                new MenuItem("Add Employees", AddEmployees),
                new MenuItem("Fire Employees", Company.FireEmployees),
                new MenuItem("Print Summary", PrintSummary),
                new MenuItem("Quit", () => ShuttingDown = true)
            })
            {
                Header = "Main Menu"
            };
            while (!ShuttingDown)
            {
                menu.ShowMenu();
            }
        }

        private static void GetDefaultEmployees()
        {
            var employees = new[]
            {
                new Employee("John", "Smith", 50000, Company),
                new Employee("Sam", "Wellington", 60000, Company),
                new Employee("Samantha", "Rutherford", 30000, Company),
                new Employee("John", "Doe", 80000, Company),
                new Employee("Casey", "Maxwell", 70000, Company),
                new Employee("Lana", "Jones", 40000, Company),
                new Employee("Peter", "Jackson", 110000, Company),
                new Employee("Sam", "Hampshire", 90000, Company),
            };
            Company.Employees.AddRange(employees);
        }

        private static void PrintSummary()
        {
            var totalYearly = 0;
            Company.PrintBanner();
            foreach (var employee in Company.Employees)
            {
                totalYearly += employee.AnnualSalary;
                Console.WriteLine(
                    $"{employee.FirstName} {employee.LastName} - {employee.Email} - {employee.MonthlySalary}/month {employee.AnnualSalary}/year");
            }

            Console.WriteLine($"We pay {totalYearly / 12} a month for our employees and {totalYearly} a year.");
        }

        private static void AddEmployees()
        {
            while (true)
            {
                Console.Write("First name: ");
                var name = Console.ReadLine();
                Console.Write("Last name: ");
                var surname = Console.ReadLine();
                Console.Write("Yearly salary: ");
                var salary = int.Parse(Console.ReadLine() ?? "0");

                Company.Employees.Add(new Employee(name, surname, salary, Company));

                Console.WriteLine("Press any key to continue or press n to stop.");
                var v = Console.ReadKey(true).KeyChar;
                if (v.ToString().ToLower() == "n")
                {
                    break;
                }
            }
        }
    }
}