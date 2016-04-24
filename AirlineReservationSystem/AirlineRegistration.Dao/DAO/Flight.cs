using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
    public class Flight
    {
        public string flightName { get; set; }
        public string flightId { get; set; }
      //  public DateTime thisDate1 = new DateTime(2011, 6, 10);
      //  public TimeSpan timeSpan1 = new TimeSpan(2, 14, 18);
        public string thisDate { get; set; }
        public string timeSpan { get; set; }
        public string flightSource { get; set; }
        public string flightDestination { get; set; }
    //    public Carrier carrier;
        public float economyPrice { get; set; }
        public float ecoPlusPrice { get; set; }
        public float businessPrice { get; set; }
     //   public FlightCrew flightcrew;
        public String carrierId { get; set; }
        public int flightCrewId { get; set; }

        public Flight(string flightName, string flightId, string thisDate, 
            string timeSpan, string flightSource, string flightDestination, 
            float economyPrice, float ecoPlusPrice, float businessPrice, 
             String carrierId, int flightCrewId){
            this.flightName = flightName;
            this.flightId = flightId;
            this.thisDate = thisDate;
            this.timeSpan = timeSpan;
            this.flightSource = flightSource;
            this.flightDestination = flightDestination;
          //  this.carrier = carrier;
            this.economyPrice = economyPrice;
            this.businessPrice = businessPrice;
         //   this.flightcrew = flightcrew;
            this.carrierId = carrierId;
            this.flightCrewId = flightCrewId;
        }
        public Flight() { }
        
        

    }
}
