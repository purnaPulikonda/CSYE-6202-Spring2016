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
    public class UserDAL : AbstractDAL
    {

        public int addUser(User user)
        {

            var cmd = getConnection().CreateCommand();
            cmd.CommandText = @"
                INSERT into users (Name, Address, phoneNumber, emailId,username)
                VALUES (@Name, @Address, @PhoneNumber, @EmailId,@username)";

            cmd.Parameters.AddWithValue("@Name", user.name);
            cmd.Parameters.AddWithValue("@Address", user.address);
            cmd.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
            cmd.Parameters.AddWithValue("@EmailId", user.emailId);
            cmd.Parameters.AddWithValue("@username", user.username);

            var number = cmd.ExecuteNonQuery();

            return number;
        }


        public int updateUser(String name, String address, String phone, String email, int userid)
        {
            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                UPDATE users
                SET Name = @Name, Address=@Address,phoneNumber=@phoneNumber,emailID=@emailID
                WHERE  userId =@userId";

            cmdOne.Parameters.AddWithValue("@Name", name);
            cmdOne.Parameters.AddWithValue("@Address", address);
            cmdOne.Parameters.AddWithValue("@phoneNumber", phone);
            cmdOne.Parameters.AddWithValue("@emailID", email);
            cmdOne.Parameters.AddWithValue("@userId", userid);


            var result = cmdOne.ExecuteNonQuery();
            return result;
        }

        public User getUserFromUsername(string username)
        {
            User usr = new User();
            var cmdOne = getConnection().CreateCommand();

            cmdOne.CommandText = @"
                Select * from users 
                WHERE  username =@usernameTwo";

            cmdOne.Parameters.AddWithValue("@usernameTwo", username);


            using (var reader = cmdOne.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine("userId: {0} name: {1} address: {2} phonenumber: {3} emailId: {4}  username: {5} ", reader.GetInt32(0),
                        reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    usr.id = reader.GetInt32(0);
                    usr.name = reader.GetString(1);
                    usr.address = reader.GetString(2);
                    usr.phoneNumber = reader.GetString(3);
                    usr.emailId = reader.GetString(4);
                    usr.username = reader.GetString(5);

                }
            }
            return usr;
        }



        public void selectAllUsers()
        {


            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                    SELECT * from users";


            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID: {0} Name: {1} Order Count: {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
            }
        }
    }
}