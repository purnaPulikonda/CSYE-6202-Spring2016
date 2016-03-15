using System;

namespace SimplePayroll
{
    public class SalariedEmployee : Employee
    {
        public float weeklySalary { get; set; }

        public SalariedEmployee(string first, string last, string ssn, float salary) : base(first, last, ssn)
        {
            this.weeklySalary = salary;
        }

        public override float MoneyEarned()
        {
            return weeklySalary;
        }

        public override string ToString()
        {
            return String.Format("\nSalaried Employee: {0} {1} \nSocial Security Number: {2} \nWeekly Salary: ${3:0.00} \nEarned: ${4:0.00} \n", firstName, lastName, ssn, weeklySalary, MoneyEarned());
        }
    }
}
