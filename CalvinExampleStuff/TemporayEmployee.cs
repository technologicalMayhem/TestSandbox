using System;

namespace CalvinExampleStuff
{
    internal class TemporaryEmployee : Employee
    {
        public DateTime EmploymentStart { get; set; }

        public DateTime EmploymentEnd { get; set; }

        public TemporaryEmployee(string firstName, string lastName, int annualSalary, Company company,
            DateTime employmentStart, DateTime employmentEnd) : base(firstName, lastName, annualSalary, company)
        {
            EmploymentStart = employmentStart;
            EmploymentEnd = employmentEnd;
        }
    }
}