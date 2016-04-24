using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    class Admin : Person
    {

        int adminId;
        string adminPrivliges;

        public Admin() :base()  { }

        public Admin(int id, string name, string address, string phonenumber, string emailId) : base( name ,address ,phonenumber,emailId) {
            this.adminId = id;

        }
    }
}
