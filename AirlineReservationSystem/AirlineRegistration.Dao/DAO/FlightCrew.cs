using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
   public class FlightCrew
    {
       public int crewId { get; set; }
        List<Employee> crew;
 
        public FlightCrew() { }
        public FlightCrew(List<Employee> crew, int crewId) {
            this.crew = crew;
            this.crewId = crewId;
        }
    }
}
