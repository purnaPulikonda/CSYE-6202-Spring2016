using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AirlineReservationSystem
{
   public class User : Person
    {

       public int id { get; set; }
       public string username { get;  set; }
        public User() :base()  { }

        public User(int id,string username,string name , string address , string phonenumber, string emailId) : base( name ,address ,phonenumber,emailId) {
            this.id=id;
            this.username = username;
        }
      
     
    }
}
