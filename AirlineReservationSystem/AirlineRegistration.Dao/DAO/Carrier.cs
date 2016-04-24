using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
   public class Carrier
    {
        public string carrierId { get; set; }
        public string carrierName { get; set; }
     
        public string carrierLocation { get; set; }

        public Carrier() { }

        public Carrier(string carrierId, string carrierName, string carrierLocation) {
            this.carrierName = carrierName;
            this.carrierId = carrierId;
            this.carrierLocation = carrierLocation;
        }
    }
}
