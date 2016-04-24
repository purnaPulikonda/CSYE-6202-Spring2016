using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineRegistration.Dao;
using AirlineReservationSystem;
using AirlineRegistration.Dao.DAL;

public class FlightBookingDAL: AbstractDAL
{
   
    public int deleteBooking(FlightBoookings fb) {


        var cmdOne = getConnection().CreateCommand();

        cmdOne.CommandText = @"
                            DELETE FROM flightBookings
                                    WHERE userid = @userid AND flightid=@flightid AND numberOfseats =@numberOfseats AND amountPerseat=@amountPerseat ";

        cmdOne.Parameters.AddWithValue("@userid", fb.user.id);
        cmdOne.Parameters.AddWithValue("@flightid", fb.flight.flightId);
        cmdOne.Parameters.AddWithValue("@numberOfseats", fb.numberOfSeats);
        cmdOne.Parameters.AddWithValue("@amountPerseat", fb.amountperSeat);

        var reader = cmdOne.ExecuteNonQuery();
        return reader;
    }

    public SqlDataReader getBookingsForUser(User user)
    {

        var cmdOne = getConnection().CreateCommand();

        cmdOne.CommandText = @"
                Select * from FlightBookings where userid =@userid";
        cmdOne.Parameters.AddWithValue("@userid", user.id);
        var reader1 = cmdOne.ExecuteReader();
        return reader1;
    }
    public SqlDataReader getBookingsForFlight(Flight flight)
    {


        var cmdOne = getConnection().CreateCommand();

        cmdOne.CommandText = @"
                Select * from flightBookings 
                WHERE  flightid =@flightid";

        cmdOne.Parameters.AddWithValue("@flightid", flight.flightId);


        var reader = cmdOne.ExecuteReader();
        return reader;
    }

    public int insertFlightBooking(FlightBoookings fb)
    {
        var cmd = getConnection().CreateCommand();
       
        cmd.CommandText = @"
                INSERT into flightBookings (userid,flightid ,numberOfseats,amountPerseat,category,username)
                VALUES (@userid, @flightid, @numberOfseats, @amountPerseat,@category,@username)";

        cmd.Parameters.AddWithValue("@userid", fb.user.id);
        cmd.Parameters.AddWithValue("@flightid", fb.flight.flightId);
        cmd.Parameters.AddWithValue("@numberOfseats", fb.numberOfSeats);
        cmd.Parameters.AddWithValue("@amountPerseat", fb.amountperSeat);
        cmd.Parameters.AddWithValue("@category", fb.category);
        cmd.Parameters.AddWithValue("@username", fb.user.name);

        var result = cmd.ExecuteNonQuery();
        return result;
    }
}
