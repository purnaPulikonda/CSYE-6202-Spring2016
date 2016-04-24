using AirlineReservationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
    public class Employee : Person
    {
        public int empId { get; set; }
        public Employee() : base() { }
        public Employee(string name, string address, string phonenumber, string email) : base(name, address, phonenumber, email) { }
    }
}
