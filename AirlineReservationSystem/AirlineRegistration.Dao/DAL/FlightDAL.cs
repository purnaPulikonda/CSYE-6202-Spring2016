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
    public class FlightDAL : AbstractDAL
    {

        public int createNewFlight(Flight f)
        {

            var cmd = getConnection().CreateCommand();
            cmd.CommandText = @"
                INSERT into flight (flightId, flightName, 
dateString, timeString,flightSource,flightDestination,economyPrice,ecoPlusPrice,businessPrice,carrierId,flightcrewId)
                VALUES (@flightId, @flightName, @dateString, @timeString,@flightSource, 
@flightDestination, @economyPrice,@ecoPlusPrice,@businessPrice,@carrierId,@flightcrewId)";

            cmd.Parameters.AddWithValue("@flightId", f.flightId);
            cmd.Parameters.AddWithValue("@flightName", f.flightName);
            cmd.Parameters.AddWithValue("@dateString", f.thisDate);
            cmd.Parameters.AddWithValue("@timeString", f.timeSpan);
            cmd.Parameters.AddWithValue("@flightSource", f.flightSource);
            cmd.Parameters.AddWithValue("@flightDestination", f.flightDestination);
            cmd.Parameters.AddWithValue("@economyPrice", f.economyPrice);
            cmd.Parameters.AddWithValue("@ecoPlusPrice", f.ecoPlusPrice);
            cmd.Parameters.AddWithValue("@businessPrice", f.businessPrice);
            cmd.Parameters.AddWithValue("@carrierId", f.carrierId);
            cmd.Parameters.AddWithValue("@flightcrewId", f.flightCrewId);



            var result = cmd.ExecuteNonQuery();
            return result;
        }


        public List<Flight> getAllFlights(List<Flight> listofFlights)
        {

            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                Select * from Flight";


            using (var reader = cmdOne.ExecuteReader())
            {
                // dataGridViewFlights.DataSource = reader;
                while (reader.Read())
                {
                    Console.WriteLine("flightId: {0} flightName: {1} dateString: {2} timeString: {3} flightSource: {4} flightDestination: {5}  economyPrice: {6} ecoPlusPrice: {7} businessPrice: {8} carrierId: {9} flightcrewId: {10}");
                    Flight f = new Flight();
                    {
                        f.flightId = reader.GetString(0);
                        f.flightName = reader.GetString(1);
                        f.thisDate = reader.GetString(2);
                        f.timeSpan = reader.GetString(3);
                        f.flightSource = reader.GetString(4);
                        f.flightDestination = reader.GetString(5);
                        f.economyPrice = (float)reader.GetDouble(6);
                        f.ecoPlusPrice = (float)reader.GetDouble(7);
                        f.businessPrice = (float)reader.GetDouble(8);
                        f.carrierId = reader.GetString(9);
                        f.flightCrewId = reader.GetInt32(10);

                    }
                    listofFlights.Add(f);

                }
            }
            return listofFlights;
        }

        public int removeFlight(Flight f)
        {
            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                            DELETE FROM flight
                                    WHERE flightid=@flightid";


            cmdOne.Parameters.AddWithValue("@flightid", f.flightName);

            var reader = cmdOne.ExecuteNonQuery();
            return reader;
        }

        public int updateFlight(Flight f)
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = @"
                UPDATE flight
                SET dateString = @dateString, timeString=@timeString,flightSource=@flightSource
                 flightDestination=@flightDestination,economyPrice=@economyPrice,
                  ecoPlusPrice=@ecoPlusPrice,businessPrice=@businessPrice,
                 carrierId=@carrierId,flightcrewId=@flightcrewId
                WHERE  flightId =@flightId AND flightName=@flightName";

            cmd.Parameters.AddWithValue("@flightId", f.flightId);
            cmd.Parameters.AddWithValue("@flightName", f.flightName);
            cmd.Parameters.AddWithValue("@dateString", f.thisDate);
            cmd.Parameters.AddWithValue("@timeString", f.timeSpan);
            cmd.Parameters.AddWithValue("@flightSource", f.flightSource);
            cmd.Parameters.AddWithValue("@flightDestination", f.flightDestination);
            cmd.Parameters.AddWithValue("@economyPrice", f.economyPrice);
            cmd.Parameters.AddWithValue("@ecoPlusPrice", f.ecoPlusPrice);
            cmd.Parameters.AddWithValue("@businessPrice", f.businessPrice);
            cmd.Parameters.AddWithValue("@carrierId", f.carrierId);
            cmd.Parameters.AddWithValue("@flightcrewId", f.flightCrewId);



            var result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
