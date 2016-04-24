using AirlineReservationSystem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineRegistration.Dao.DAL
{
    public abstract class AbstractDAL
    {
      public  String SQLConnectionString;
      public  SqlConnection connection = null;

        public void openConnection()
        {
            SQLConnectionString = Helper.GetConnectionStringFromExeConfig
                        ("ConnString");
            connection = new SqlConnection(SQLConnectionString);
            connection.Open();

        }
        public void closeConnection()
        {
            connection.Close();
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

    }
}
