    namespace CalvinExampleStuff
{
    internal class Employee
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email => $"{FirstName[0]}.{LastName}@{Company.GetNameForEmail()}.com".ToLower();
        public int AnnualSalary { get; }
        public int MonthlySalary => AnnualSalary / 12;
        private Company Company { get; }

        public Employee(string firstName, string lastName, int annualSalary, Company company)
        {
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
            Company = company;
        }
    }
}