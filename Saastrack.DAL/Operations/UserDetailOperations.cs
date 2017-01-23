using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class UserDetailOperations
    {
        public static string GetNextDbName()
        {
            string returnDbName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 DatabaseId FROM userdetails where Locked=0 order by InitialProcessed,LastProcessed", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                    }
                }
            }
            return returnDbName;
        }

        public static string GetNextDbNameForTransactionsFromPlaid()
        {
            string returnDbName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 DatabaseId FROM userdetails order by InitialPlaid,LastProcessedPlaid", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                    }
                }
            }
            return returnDbName;
        }

        public static void LockDb(string dbName)
        {            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                string sql = "UPDATE dbo.UserDetails SET Locked=1 where DatabaseId = '" + dbName + "';";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }
            }
        }

        public static void UpdateLastProcessedAndLocked(string dbName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();               
                //insert the new tool with logo etc                
                string sql = "UPDATE dbo.UserDetails SET Locked=0, InitialProcessed=1, LastProcessed=@param1 where DatabaseId = @param2;";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = dbName;                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }
            }
        }

        public static void UpdatePlaidLastProcessedAndLocked(string dbName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                //insert the new tool with logo etc                
                string sql = "UPDATE dbo.UserDetails SET InitialPlaid=1, LastProcessedPlaid=@param1 where DatabaseId = @param2;";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = dbName;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }
            }
        }

        public static string GetUserDbName(string userId)
        {
            string returnDbName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT DatabaseId FROM userdetails where UserId = '" + userId + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                    }
                }
            }
            return returnDbName;
        }

        public static string GetUserByEmail(string email)
        {
            string returnDbName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT DatabaseId FROM Employees where Email = '" + email + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                    }
                }
            }
            return returnDbName;
        }

        public static bool HasInitialProcessed(string userId)
        {
            bool returnDbName = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT InitialProcessed FROM userdetails where UserId = '" + userId + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetBoolean(0);
                    }
                }
            }
            return returnDbName;
        }

        public static List<Guid> GetAllDatabaseGuids()
        {
            List<Guid> guidList = new List<Guid>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT DatabaseId FROM userdetails", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid newGuid = Guid.Parse(reader.GetString(0));
                        if (!guidList.Contains(newGuid))
                        {
                            guidList.Add(newGuid);
                        }
                    }
                }
            }

            return guidList;
        }

        public static Dictionary<Guid,string> GetAllDatabaseUsers()
        {
            Dictionary<Guid, string> guidList = new Dictionary<Guid, string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id,Email FROM aspnetusers", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid newGuid = Guid.Parse(reader.GetString(0));
                        string email = reader.GetString(1);

                        guidList.Add(newGuid, email);
                        
                    }
                }
            }

            return guidList;
        }

    }
}
