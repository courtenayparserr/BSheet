using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class BaseOperations
    {
        public static string connectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["UserDb"].ToString();
            }
        }

        public static string connectionStringWithUser(string user)
        {
            string retval = ConfigurationManager.ConnectionStrings["DefaultConnectionUserDb"].ToString().Replace("{dbname}", user);
            return retval;
        }

        public static string GetConnectionString()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["StorageConnectionString"];
                if (string.IsNullOrEmpty(connString))
                {
                    return "UseDevelopmentStorage=true";
                }
                else
                {
                    return connString;
                }

            }
            catch (Exception)
            {
                return "UseDevelopmentStorage=true";
            }
        }
    }
}
