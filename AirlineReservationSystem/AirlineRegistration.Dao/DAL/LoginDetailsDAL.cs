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
   public class LoginDetailsDAL: AbstractDAL
    {
      

        public int addLoginUser(LoginDetails lg, User user)
        {
            var cmdOne = getConnection().CreateCommand();
          

            cmdOne.CommandText = @"
                INSERT into LoginDetails (username, password, logintype)
                VALUES (@usernameOne, @password, @logintype)";

            cmdOne.Parameters.AddWithValue("@usernameOne", user.username);
            cmdOne.Parameters.AddWithValue("@password", lg.password);
            cmdOne.Parameters.AddWithValue("@logintype", "u");



            var number = cmdOne.ExecuteNonQuery();
            return number;
        }

        public string isValidLogin(string username,string password) {

            string logintype="";
                var cmdOne = getConnection().CreateCommand();

                cmdOne.CommandText = @"
                Select * from LoginDetails 
                WHERE  username =@usernameOne AND password = @password";

                cmdOne.Parameters.AddWithValue("@usernameOne", username);
                cmdOne.Parameters.AddWithValue("@password", password);


             

                using (var reader = cmdOne.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("username: {0} password: {1} logintype: {2}", reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        logintype = reader.GetString(2);
                        username = reader.GetString(0);
                        password = reader.GetString(1);
                    }
                }
            return logintype;
            }
    }

}
