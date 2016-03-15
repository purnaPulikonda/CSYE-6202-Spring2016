using System;

namespace SimplePayroll
{
    public class ComissionEmployee : Employee
    {
        public float commissionRate { get; set; }
        public float grossSales { get; set; }

        public ComissionEmployee(string first, string last, string ssn, float commissionRate, float grossSales) : base(first, last, ssn)
        {
            this.commissionRate = commissionRate;
            this.grossSales = grossSales;
        }

        public float MoneyFromCommission()
        {
            return (commissionRate * grossSales);
        }
        public override float MoneyEarned()
        {
            return MoneyFromCommission();
        }
        public override string ToString()
        {
            return String.Format("\nCommission Employee: {0} {1} \nSocial Security Number: {2}  \nComission Rate: {3} \nGross Sales: ${4:0.00}  \nEarned: ${5:0.00} \n", firstName, lastName, ssn, commissionRate, grossSales, MoneyEarned());
        }
    }
}
