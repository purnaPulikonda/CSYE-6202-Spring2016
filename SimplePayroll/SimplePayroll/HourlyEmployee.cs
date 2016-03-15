using System;


namespace SimplePayroll
{
    class HourlyEmployee : Employee
    {

        private float hourlyWage { get; set; }
        private float numberOfHoursWorked { get; set; }


        public HourlyEmployee(string first, string last, string ssn, float hourlyWage, float numberOfHoursWorked) : base(first, last, ssn)
        {
            this.hourlyWage = hourlyWage;
            this.numberOfHoursWorked = numberOfHoursWorked;
        }
        public override float MoneyEarned()
        {
            if (numberOfHoursWorked <= 40)
            {
                return numberOfHoursWorked * hourlyWage;
            }
            else {
                return ((40 * hourlyWage) + ((numberOfHoursWorked - 40) * hourlyWage * 1.5f));
            }
        }
        public override string ToString()
        {
            return String.Format("\nHourly Employee: {0} {1} \nSocial Security Number: {2} \nHourly Wage: ${3:0.00} , Hourly Worked: {4}  \nEarned: ${5:0.00} \n", firstName, lastName, ssn, hourlyWage, numberOfHoursWorked, MoneyEarned());
        }
    }

}
