using System;


namespace SimplePayroll
{
    public class SalaryBasedCommissionEmployee : ComissionEmployee
    {

        float baseSalary { get; set; }
        public SalaryBasedCommissionEmployee(string first, string last, string ssn, float commissionRate, float grossSales, float Salary) : base(first, last, ssn, commissionRate, grossSales)
        {

            baseSalary = Salary;
        }


        public override float MoneyEarned()
        {
            return baseSalary + base.MoneyFromCommission();
        }

        public override string ToString()
        {
            return String.Format("\nBase Salaried Commissioned Employee: {0} {1} \nSocial Security Number: {2}  \nComission Rate: {3} \nGross Sales: ${4:0.00} , Base Salary: ${5:0.00} \nEarned: ${6:0.00} \n", firstName, lastName, ssn, commissionRate, grossSales, baseSalary, MoneyEarned());
        }

    }


}
