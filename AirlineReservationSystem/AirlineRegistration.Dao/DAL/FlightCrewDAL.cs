using AirlineRegistration.Dao.DAL;
using AirlineReservationSystem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao
{
    public class FlightCrewDAL : AbstractDAL
    {
     
        public void createNewCrew(string crewid, List<Employee> listOfEmployee)
        {
            foreach (Employee e in listOfEmployee)
            {
                var cmd = getConnection().CreateCommand();
                cmd.CommandText = @"
                INSERT into crewEmployee (crewid, empid)
                VALUES (@crewid, @empid)";

                cmd.Parameters.AddWithValue("@crewid", crewid);
                cmd.Parameters.AddWithValue("@empid", e.empId);

                cmd.ExecuteNonQuery();
            }

            foreach (Employee e in listOfEmployee)
            {
                var cmdOne = getConnection().CreateCommand();
                cmdOne.CommandText = @"
                UPDATE Employee
                SET crewid=@crewid
                WHERE empid=@empid";

                cmdOne.Parameters.AddWithValue("@crewid", crewid);
                cmdOne.Parameters.AddWithValue("@empid", e.empId);

                cmdOne.ExecuteNonQuery();
            }
        }


        public SqlDataReader getCrewByCrewid(int flightCrewId)
        {

            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                Select * from employee 
                WHERE  crewId =@crewId";

            cmdOne.Parameters.AddWithValue("@crewId", flightCrewId);


            var reader = cmdOne.ExecuteReader();

            return reader;

        }

        public int removeCrew(FlightCrew flightCrew)
        {
            var cmdOne = connection.CreateCommand();

            cmdOne.CommandText = @"
                            DELETE FROM crewEmployee
                                    WHERE crewId=@crewId";


            cmdOne.Parameters.AddWithValue("@crewId", flightCrew.crewId);

            var reader = cmdOne.ExecuteNonQuery();
            return reader;
        }
    }

}
