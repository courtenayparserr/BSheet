using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Configuration;

namespace Saastrack.DAL.Operations
{
    public class AccountOperations: BaseOperations
    {
        public static List<string> GetDiscludedWords()
        {
            List<string> returnDbName = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT Name FROM DiscludedWords", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName.Add(reader.GetString(0));
                    }
                }
            }
            return returnDbName;
        }

        public static List<string> GetIncludedWords()
        {
            List<string> returnDbName = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("select Name from dbo.MotherSubscriptions", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName.Add(reader.GetString(0));
                    }
                }
            }
            return returnDbName;
        }

        public static Account InsertAccount(string databaseName, Account account, int cardId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                if (!db.Accounts.Any(e => e._id == account._id))
                {
                    account = db.Accounts.Add(account);
                    db.SaveChanges();
                }
                else
                {
                    //var result = db.Accounts.SingleOrDefault(e => e._id == account._id);
                    //db.Database.ExecuteSqlCommand(@"UPDATE account SET ownerid = '" + ownerItem.id + "' WHERE id = " + site.id + ";");
                    //db.SaveChanges();                    
                }
            }

            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                db.Database.ExecuteSqlCommand(@"UPDATE account SET Card_id = " + cardId + " WHERE id = " + account.Id + ";");
                db.SaveChanges();
            }
            return account;
        }

        public static void AddLastAddedDateForAccount(string databaseName, DateTime lastUpdated, int accountid)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var account = db.Accounts.Where(d => d.Id == accountid);
                if (account != null)
                {
                    var theAcc = account.First();
                    theAcc.lastdateadded = lastUpdated;
                    db.SaveChanges();
                }
            }
        }

        public static void UpdateDashboardForAccount(string databaseName, Account accountUp)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var account = db.Accounts.Where(d => d.Id == accountUp.Id);
                if (account != null)
                {
                    var theAcc = account.First();
                    theAcc.Annual = accountUp.Annual;
                    theAcc.Monthly = accountUp.Monthly;
                    theAcc.MonthlyIncreaseDecrease = accountUp.MonthlyIncreaseDecrease;
                    theAcc.AnnualIncreaseDecrease = accountUp.AnnualIncreaseDecrease;
                    theAcc.Subs = accountUp.Subs;
                    db.SaveChanges();
                }
            }
        }

        public static List<Account> GetAllAccountsForUser(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.Accounts.Where(m => m.enabled == true).ToList();
            }
        }

        public static List<Account> GetAllAccountsForUserInInstitutionOrder(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.Accounts.OrderBy(m => m.institution_type).ToList();
            }
        }

        public static List<Account> GetAllAccountsIncludingDisabledForUser(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.Accounts.ToList();
            }
        }

        public static Account GetAccount(string databaseName, int accId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.Accounts.Where(m => m.Id == accId).First();
            }
        }

        public void Update(Account obj,string databaseName, params Expression<Func<Account, object>>[] propertiesToUpdate)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                db.Set<Account>().Attach(obj);

                foreach (var p in propertiesToUpdate)
                {
                    db.Entry(obj).Property(p).IsModified = true;
                }
            }
        }
        
    }
}
