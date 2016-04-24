using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
   public class LoginDetails
    {
       public string username { get; set; }
       public string password { get; set; }
        public string loginType { get; set; }

        public LoginDetails(string username, string password,string loginType) {
            this.username = username;
            this.password = password;
            this.loginType = loginType;
        }
       public  LoginDetails() { }
    }
}
 