using System;
using System.Configuration;   // System.Configuration.dll
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace AirlineReservationSystem
{
    public static class Helper
    {
        public static string GetConnectionStringFromExeConfig(string connectionStringNameInConfig)
        {
            ConnectionStringSettings connectionStringSettings =
                ConfigurationManager.ConnectionStrings[connectionStringNameInConfig];

            if (connectionStringSettings == null)
            {
                throw new ApplicationException(String.Format
                    ("Error. Connection string not found for name '{0}'.",
                    connectionStringNameInConfig));
            }
            return connectionStringSettings.ConnectionString;
        }



        public static bool ValidateName(string userInputName)
        {
            if (String.IsNullOrEmpty(userInputName))
                return false;

            string pattern = @"^[a-zA-Z]+$";
            return Regex.IsMatch(userInputName, pattern);
        }

        public  static bool ValidateEmail(string name)
        {
            try
            {
                MailAddress ma = new MailAddress(name);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateAddress(string name)
        {

            if (String.IsNullOrEmpty(name))
                return false;
            else return true; 
        }

        public static bool ValidatePhoneNumber(string number)
        {
           

            if (String.IsNullOrEmpty(number))
                return false;
            return true;

           // string pattern = @"^\d{10}$";
           // return Regex.IsMatch(number, pattern);
        }
    }


       
      
    }

