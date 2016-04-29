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
    public class CarrierDAL : AbstractDAL
    {

        public Carrier getCarrierFromID(string carrierid)
        {
            Carrier carrier = new Carrier();
            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                Select * from Carrier 
                WHERE  carrierId =@carrierId";

            cmdOne.Parameters.AddWithValue("@carrierId", carrierid);


            using (var reader = cmdOne.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine("carrierId: {0} carrierName: {1} carrierLocation: {2}", reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    carrier.carrierId = "Carrier Id:\t" + reader.GetString(0);
                    carrier.carrierName = "Carrier Name \t" + reader.GetString(1);
                    carrier.carrierLocation = "Carrier Location" + reader.GetString(2);
                }
            }
            return carrier;
        }

        public int updateCArrier(Carrier c)
        {
            var cmdOne = connection.CreateCommand();

            cmdOne.CommandText = @"
                UPDATE carrier
                SET carrierName = @carriername, carrierLocation=@carrierLocation
                WHERE  carrierId =@carrierId";

            cmdOne.Parameters.AddWithValue("@carrierId", c.carrierId);
            cmdOne.Parameters.AddWithValue("@carriername", c.carrierName);
            cmdOne.Parameters.AddWithValue("@carrierLocation", c.carrierLocation);



            var result = cmdOne.ExecuteNonQuery();
            return result;
        }

        public int removeCarrier(Carrier c)
        {
            var cmdTwo = connection.CreateCommand();

            cmdTwo.CommandText = @"
                            DELETE FROM carrier
                                    WHERE carrierid=@carrierid";


            cmdTwo.Parameters.AddWithValue("@carrierid", c.carrierId);

            var reader = cmdTwo.ExecuteNonQuery();
            return reader;
        }

        public int addCarrier(Carrier carrier)
        {

            var cmd = connection.CreateCommand();

            cmd.CommandText = @"
                INSERT into carrier (carrierId, carrierName, carrierLocation)
                VALUES (@carrierId, @carrierName, @carrierLocation)";

            cmd.Parameters.AddWithValue("@carrierId", carrier.carrierId);
            cmd.Parameters.AddWithValue("@carrierName", carrier.carrierName);
            cmd.Parameters.AddWithValue("@carrierLocation", carrier.carrierLocation);
            int result = 0;

            result = cmd.ExecuteNonQuery();



            return result;
        }

    }
}
