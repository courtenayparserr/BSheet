using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class CompanyUserOperations: BaseOperations
    {        
        public static CompanyUser GetUser(string databaseName, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.CompanyUsers.Where(m => m.email == email).FirstOrDefault();
            }
        }

        public static CompanyUser GetUserById(string databaseName, int userId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.CompanyUsers.Where(m => m.id == userId).Include(d => d.services).FirstOrDefault();
            }
        }

        public static bool HasSeenIntros(string databaseName, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var hasSeen = db.CompanyUsers.Where(m => m.email == email).FirstOrDefault().hasSeenIntro;
                if (hasSeen != null && hasSeen.HasValue)
                {
                    return hasSeen.Value;
                }
                return false;
            }
        }

        public static void UpdateHasSeenIntro(string databaseName, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var account = db.CompanyUsers.Where(m => m.email == email);
                if (account != null)
                {
                    var theAcc = account.First();
                    theAcc.hasSeenIntro = true;                    
                    db.SaveChanges();
                }
            }
        }

        public static void UpdateHasSeenIntroEmployee(string databaseName, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var account = db.CompanyUsers.Where(m => m.email == email);
                if (account != null)
                {
                    var theAcc = account.First();
                    theAcc.hasSeenIntroEmployee = true;
                    db.SaveChanges();
                }
            }
        }

        public static bool HasSeenIntrosEmployee(string databaseName, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var hasSeen = db.CompanyUsers.Where(m => m.email == email).FirstOrDefault().hasSeenIntroEmployee;
                if (hasSeen != null && hasSeen.HasValue)
                {
                    return hasSeen.Value;
                }
                return false;
            }
        }

        public static void RemoveById(string databaseName, int userId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var user = db.CompanyUsers.Where(m => m.id == userId).FirstOrDefault();
                db.CompanyUsers.Remove(user);
                db.SaveChanges();
            }
        }

        public static List<CompanyUser> GetAllUsers(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.CompanyUsers.Include(i => i.services).ToList();
            }
        }

        public static void InsertInitialCompanyUser(string databaseName, Company company, CompanyUser user)
        {
            Company comp = null;
            // add to central db too
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                company.CompanyUsers = new List<CompanyUser>();
                company.CompanyUsers.Add(user);
                comp = db.Company.Add(company);
                db.SaveChanges();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();                
                string sql = "INSERT INTO Employees (Email, DatabaseId) " +
"VALUES (@Email, @DBID);";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.email;
                    cmd.Parameters.Add("@DBID", SqlDbType.NVarChar).Value = databaseName;                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }
                
            }

        }

        public static List<CompanyUserServices> GetUsersForService(string databaseName, int serviceId, DateTime startDate, DateTime endDate)
        {
            List<CompanyUserServices> returnDbName = new List<CompanyUserServices>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var userServices = db.CompanyUserService.Include(n=>n.companyuser).Where(m => m.serviceid == serviceId && m.DateAdded >= startDate).ToList();                
                foreach (CompanyUserServices serv in userServices)
                {
                    var servicesUsage = db.ServiceUsage.Where(s => s.serviceid == serviceId && s.UsageDate >= startDate && s.UsageDate <= endDate && s.companyuserid == serv.companyuserid).Sum(d => d.Seconds);
                    if(servicesUsage > 0)
                    {
                        serv.TotalMinutesSpent = Convert.ToInt32((double)servicesUsage / 60.0);
                    }
                    else
                    {
                        serv.TotalMinutesSpent = 0;
                    }
                    //get last login
                    var lastLogin = db.ServiceUsage.Where(s => s.serviceid == serviceId && s.companyuserid == serv.companyuserid).OrderByDescending(d => d.UsageDate);
                    if(lastLogin != null)
                    {
                        serv.LastLogin = lastLogin.First().UsageDate;
                    }
                    returnDbName.Add(serv);
                }                
            }

            return returnDbName;
        }

        public static void UpdateCompanyUser(string databaseName, CompanyUser user)
        {         
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var account = db.CompanyUsers.Where(d => d.id == user.id);
                if (account != null)
                {
                    var theAcc = account.First();
                    theAcc.firstName = user.firstName;
                    theAcc.lastName = user.lastName;
                    theAcc.phone = user.phone;
                    theAcc.email = user.email;
                    db.SaveChanges();
                }
            }
        }

        public static Dictionary<string,string> CheckComputerNameAndInsertIfNotExists(string computername)
        {
            string returnDbName = string.Empty;
            string email = string.Empty;
            Dictionary<string, string> returnItem = new Dictionary<string, string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT DatabaseId, Email FROM employees where ComputerName = '" + computername + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                        email = reader.GetString(1);
                        if(returnDbName != string.Empty)
                        {
                            returnItem.Add(returnDbName, email);
                        }                        
                    }
                }

                if(returnDbName == string.Empty)
                {                    
                    string sql = "INSERT INTO Employees (Computername) " +
    "VALUES (@CompName);";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@Compname", SqlDbType.NVarChar).Value = computername;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteScalar();
                    }
                }
            }
            return returnItem;
        }

        public static CompanyUser InsertInitialCompanyUser(string databaseName, CompanyUser user)
        {
            // add to central db too
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                user = db.CompanyUsers.Add(user);
                db.SaveChanges();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                string sql = "INSERT INTO Employees (Email, DatabaseId) " +
"VALUES (@Email, @DBID);";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.email;
                    cmd.Parameters.Add("@DBID", SqlDbType.NVarChar).Value = databaseName;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }

            }

            return user;

        }
        
    }
}
