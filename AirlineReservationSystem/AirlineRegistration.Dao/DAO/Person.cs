using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    public abstract class Person
    {

        public Person() { }
        public Person( string name, string address, string phonenumber, string email)
        {
          
            this.name = name;
            this.address = address;
            this.phoneNumber = phonenumber;
            this.emailId = email;
         
        }
    
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string emailId { get; set; }

    }


}
