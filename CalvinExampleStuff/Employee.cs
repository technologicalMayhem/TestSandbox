namespace CalvinExampleStuff
{
    internal class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email => $"{FirstName[0]}.{LastName}@{Company.GetNameForEmail()}.com".ToLower();
        public int AnnualSalary { get; set; }
        public int MonthlySalary => AnnualSalary / 12;
        public Company Company { get; set; }

        public Employee(string firstName, string lastName, int annualSalary, Company company)
        {
            Company = company;
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
        }
    }
}