using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayroll
{
    public abstract class Employee
    {
       public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string ssn { get; private set; }


        public Employee(string firstName, string lastName, string ssn) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.ssn = ssn;
        }
        public abstract float MoneyEarned();

    }
}
