using AirlineReservationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
    public class FlightBoookings
    {
      public  User user;
        public Flight flight;
        public int numberOfSeats;
        public string category;
        public float amountperSeat;

        public FlightBoookings() { }
       public  FlightBoookings(User user, Flight flight,int numberOfSeats,string category,float amountperSeat) {
            this.user = user;
            this.flight = flight;
            this.numberOfSeats = numberOfSeats;
            this.category = category;
            this.amountperSeat = amountperSeat;
        }

    }
}
