using ConsoleAppUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalvinExampleStuff
{
    class Program
    {
        public static Company Company { get; set; }
        static void Main(string[] args)
        {
            Console.Write("Give your company a name: ");
            string companyName = Console.ReadLine();
            Company = new Company(companyName);
            GetDefaultEmployees();
            var menu = new Menu(new []
            {
                new MenuItem("Add Employees", AddEmployees),
                new MenuItem("Fire Employees", Company.FireEmployes),
                new MenuItem("Print Summary", PrintSummary),
                new MenuItem("Quit", () => Environment.Exit(0))
            });
            menu.Header = "Main Menu";
            while(true)
            {
                menu.ShowMenu();
            }
        }

        private static void ShowMenu()
        {
            var actions = new List<(string, Action)>()
            {
                ("Add Employees", AddEmployees),
                ("Fire Employees", Company.FireEmployes),
                ("Print Summary", PrintSummary),
                ("Quit", () => Environment.Exit(0))
            };

            var num = 0;
            foreach (var action in actions)
            {
                Console.WriteLine($"{++num}. {action.Item1}");
            }

            Console.WriteLine();
            Console.Write($"Please make your choice [1 - {actions.Count}]: ");
            var choice = 0;
            while (true)
            {
                string text = Console.ReadLine();
                if(text.ToCharArray().All(x => char.IsNumber(x)))
                {
                    choice = int.Parse(text);
                    if (choice > 0 && choice < actions.Count + 1)
                    {
                        break;
                    }
                }
                Console.Write($"Please enter a valid number [1 - {actions.Count}]: ");
            }
            actions[choice - 1].Item2.Invoke();
        }

        private static void GetDefaultEmployees()
        {
            Employee[] employees = new Employee[]
            {
                new Employee("John", "Smith", 50000, Company),
                new Employee("Sam", "Wellington", 60000, Company),
                new Employee("Samantha", "Coolman", 30000, Company),
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
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Email} - {employee.MonthlySalary}/month {employee.AnnualSalary}/year");
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
                var salary = int.Parse(Console.ReadLine());

                Company.Employees.Add(new Employee(name, surname, salary, Company));

                Console.WriteLine("Press any key to continue or press n to stop.");
                char v = Console.ReadKey(true).KeyChar;
                if (v.ToString().ToLower() == "n")
                {
                    break;
                }
            }
        }
    }
}