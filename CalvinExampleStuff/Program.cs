using System;
using System.Collections.Generic;

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
            GetDefaultEmployess();
            ShowMenu();
            //AddEmployees(company);
            PrintSummary();
            Company.FireEmployes();
            PrintSummary();
        }

        private static void ShowMenu()
        {
            //TODO: Turn this into a List<Tuple<string, Action>>
            Dictionary<string, Action> actions = new Dictionary<string, Action>();
            actions.Add("Add Employees", AddEmployees);
            actions.Add("Fire Employees", Company.FireEmployes);
            actions.Add("Print Summary", PrintSummary);
            actions.Add("Quit", () => Environment.Exit(0));

            var n = 0;
            foreach (var action in actions)
            {
                Console.WriteLine($"{++n}. {action.Key}");
            }
            Console.WriteLine();
            Console.Write("Please make your choice of what you wanna do: ");
            var result = Console.ReadLine();
            //TODO: Do error handling if no number is provided
            var resultNum = int.Parse(result);
            //TODO: Make the thing get the right thing out of the list of actions
        }

        private static void GetDefaultEmployess()
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

    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get => $"{FirstName[0]}.{LastName}@{Company.GetNameForEmail()}.com".ToLower(); }
        public int AnnualSalary { get; set; }
        public int MonthlySalary => AnnualSalary / 12;
        public Company Company { get; set; }

        public Employee(string firstName, string lastName, int annualSalary, Company company)
        {
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
            Company = company;
        }
    }

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
            List<Employee> employeesToFire = Employees.FindAll(x => x.FirstName == firstName);
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