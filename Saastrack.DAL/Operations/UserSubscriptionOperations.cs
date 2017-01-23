using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Saastrack.DAL.Helper;
using System.Text.RegularExpressions;

namespace Saastrack.DAL.Operations
{
    public class UserSubscriptionOperations : BaseOperations
    {
        public static List<UserSubscription> GetSubscriptions(string databaseName)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .ThenByDescending(m => m.motherSubscriptionId).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.motherSubscriptionId != null && sub.motherSubscriptionId != 0)
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                            {
                                con.Open();
                                using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS WHERE MS.Id =" + sub.motherSubscriptionId, con))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        sub.MotherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                        sub.MotherSubscriptionName = reader["Name"].ToString();
                                    }
                                }
                            }
                        }
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetActiveSubscriptions(string databaseName)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .ThenByDescending(m => m.motherSubscriptionId).Where(m => m.IsActive == true && m.motherSubscriptionId != 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS WHERE MS.Id =" + sub.motherSubscriptionId, con))
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    sub.MotherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                    sub.MotherSubscriptionName = reader["Name"].ToString();
                                }
                            }
                        }
                        
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetInActiveSubscriptions(string databaseName)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .ThenByDescending(m => m.motherSubscriptionId).Where(m => m.IsActive == false && m.motherSubscriptionId != 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS WHERE MS.Id =" + sub.motherSubscriptionId, con))
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    sub.MotherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                    sub.MotherSubscriptionName = reader["Name"].ToString();
                                }
                            }
                        }
                        
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetRecurringSubscriptions(string databaseName)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .Where(m => m.IsActive == true && m.motherSubscriptionId == 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetInactiveRecurringSubscriptions(string databaseName)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .Where(m => m.IsActive == false && m.motherSubscriptionId == 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetRecurringSubscriptionsForAcc(string databaseName, int accId)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .Where(m => m.IsActive == true && m.accountid == accId && m.motherSubscriptionId == 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {                        
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetInactiveRecurringSubscriptionsForAcc(string databaseName, int accId)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .Where(m => m.IsActive == false && m.accountid == accId && m.motherSubscriptionId == 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetActiveSubscriptionsForAcc(string databaseName, int accId)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .ThenByDescending(m => m.motherSubscriptionId).Where(m => m.IsActive == true 
                        && m.accountid == accId && m.motherSubscriptionId != 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.motherSubscriptionId != null && sub.motherSubscriptionId != 0)
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                            {
                                con.Open();
                                using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS WHERE MS.Id =" + sub.motherSubscriptionId, con))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        sub.MotherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                        sub.MotherSubscriptionName = reader["Name"].ToString();
                                    }
                                }
                            }
                        }
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static List<UserSubscription> GetInActiveSubscriptionsForAcc(string databaseName, int accId)
        {
            List<UserSubscription> subscriptions = new List<UserSubscription>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription
                    .Include(m => m.account)
                    .Include(n => n.payments)
                    .OrderBy(n => n.Name)
                    .ThenByDescending(m => m.motherSubscriptionId).Where(m => m.IsActive == false && m.accountid == accId
                    && m.motherSubscriptionId != 0).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var sub in item)
                    {
                        if (sub.motherSubscriptionId != null && sub.motherSubscriptionId != 0)
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                            {
                                con.Open();
                                using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS WHERE MS.Id =" + sub.motherSubscriptionId, con))
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        sub.MotherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                        sub.MotherSubscriptionName = reader["Name"].ToString();
                                    }
                                }
                            }
                        }
                        if (sub.payments.Count() > 0)
                        {
                            sub.LastPaymentAmount = sub.payments.First().Amount;
                        }
                        subscriptions.Add(sub);
                    }

                }
            }

            return subscriptions;
        }

        public static void UpdateDateCreatedForSubscription(string databaseName, int us_id, DateTime subscriptionDateCreated)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription.Where(m => m.Id == us_id).ToList();
                if (item != null && item.Count() > 0)
                {
                    var theAcc = item.First();
                    theAcc.DateCreated = subscriptionDateCreated;
                    db.SaveChanges();

                }
            }
        }

        public static void UpdatePeriodForSubscription(string databaseName, int us_id)
        {
            //get first payment 
            //get last payment
            //calculate days btn
            //calculate number of payments
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription.Where(m => m.Id == us_id).Include(u => u.payments).ToList();
                if (item != null && item.Count() > 0)
                {
                    var firstPayment = item.First().payments.OrderBy(d => d.Date);
                    if (firstPayment != null && firstPayment.Count() > 0)
                    {
                        var firstPaymentItem = firstPayment.First().Date;
                        var lastPayment = item.First().payments.OrderByDescending(d => d.Date);
                        var lastPaymentItem = lastPayment.First().Date;

                        int numPayments = firstPayment.Count();
                        double days = (lastPaymentItem - firstPaymentItem).TotalDays;

                        double result = days / numPayments;
                        if (result > 20 && result < 40)
                        {
                            var theAcc = item.First();
                            theAcc.Period = 1; //monthly
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public static void UpdateIsActiveForSubscription(string databaseName, int us_id)
        {
            //its only active if a recent payment has been made
            //if its monthly, then in previous 35 days
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription.Where(m => m.Id == us_id).Include(u => u.payments).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var subs in item)
                    {
                        var latestPayment = subs.payments.OrderByDescending(d => d.Date);
                        if (latestPayment != null && latestPayment.Count() > 0)
                        {
                            var lastPayment = latestPayment.First().Date;
                            if (lastPayment.AddDays(30) < DateTime.Now)
                            {
                                //not in past 30 days
                                var theAcc = item.First();
                                theAcc.IsActive = false;

                            }
                            else
                            {
                                //in past 30 days
                                var theAcc = item.First();
                                theAcc.IsActive = true;
                            }

                            db.SaveChanges();

                        }
                    }
                }
            }

        }

        public static void UpdatePreviousBillingForSubscription(string databaseName, int us_id)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscription.Where(m => m.Id == us_id).Include(u => u.payments).ToList();
                if (item != null && item.Count() > 0)
                {
                    foreach (var subs in item)
                    {
                        var latestPayment = subs.payments.OrderByDescending(d => d.Date);
                        if (latestPayment != null && latestPayment.Count() > 0)
                        {
                            var theAcc = item.First();
                            theAcc.PreviousBillingDate = latestPayment.First().Date;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public static UserSubscriptionPayment AddPaymentToUserSubscription(string databaseName, UserSubscriptionPayment payment, int up_id)
        {
            int newId = 0;
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserSubscriptionPayment.Where(m => m.TransactionId == payment.TransactionId).ToList();
                if (item != null && item.Count() > 0)
                {
                    newId = item.First().Id;
                }
            }
            if (newId == 0)
            {
                using (SqlConnection con = new SqlConnection(connectionStringWithUser(databaseName)))
                {
                    con.Open();
                    //insert the new tool with logo etc                
                    string sql = "INSERT INTO usersubscriptionpayment(Date,amount,transactionid,UserSubscription_Id) VALUES(@param1,@param2,@param3,@param4); SELECT SCOPE_IDENTITY()";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.DateTime).Value = payment.Date;
                        cmd.Parameters.Add("@param2", SqlDbType.Float).Value = payment.Amount;
                        cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = payment.TransactionId;
                        cmd.Parameters.Add("@param4", SqlDbType.Int).Value = up_id;
                        cmd.CommandType = CommandType.Text;

                        newId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }

            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.UserSubscriptionPayment.Where(d => d.Id == newId).First();
            }
        }
        public static double GetTotalValueForSubscriptionForDates(string databaseName, DateTime startDate, DateTime endDate)
        {
            double totalValue = 0;
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var userSubscriptions = db.UserSubscription.Where(m => m.IsActive == true).Include(d => d.payments).ToList();
                if (userSubscriptions != null && userSubscriptions.Count() > 0)
                {
                    foreach (UserSubscription sub in userSubscriptions)
                    {
                        if (sub.payments != null && sub.payments.Count() > 0)
                        {
                            foreach (UserSubscriptionPayment payment in sub.payments)
                            {
                                if (payment.Date >= startDate && payment.Date <= endDate)
                                {
                                    totalValue += payment.Amount;
                                }
                            }
                        }
                    }
                }
            }
            return totalValue;
        }

        public static double GetTotalValueForSubscriptionForDatesAndAccount(string databaseName, DateTime startDate, DateTime endDate, Account account)
        {
            double totalValue = 0;
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var userSubscriptions = db.UserSubscription.Where(m => m.accountid == account.Id && m.IsActive == true).Include(d => d.payments).ToList();
                if (userSubscriptions != null && userSubscriptions.Count() > 0)
                {
                    foreach (UserSubscription sub in userSubscriptions)
                    {
                        if (sub.payments != null && sub.payments.Count() > 0)
                        {
                            foreach (UserSubscriptionPayment pay in sub.payments)
                            {
                                if (pay.Date >= startDate && pay.Date <= endDate)
                                    totalValue += pay.Amount;
                            }
                        }
                    }

                }
            }
            return totalValue;
        }

        public static UserSubscriptionPayment GetFirstPaymentForSubscription(string databaseName, int subId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var subscriptionPayments = db.UserSubscription.Where(m => m.Id == subId).First().payments;
                if (subscriptionPayments != null && subscriptionPayments.Count > 0)
                {
                    return subscriptionPayments.OrderBy(d => d.Date).First();
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        public static int GetTotalNumberOfSubscriptionsForDates(string databaseName, DateTime startDate, DateTime endDate)
        {
            int totalValue = 0;
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var items = db.UserSubscription.Where(m => m.DateCreated >= startDate && m.DateCreated <= endDate).ToList();
                if (items != null && items.Count() > 0)
                {
                    foreach (UserSubscription pay in items)
                    {
                        //totalValue += pay.Amount;
                    }
                }
            }
            return totalValue;
        }

        private static MotherSubscription GetMotherSubForTransactionName(string transactionName)
        {
            MotherSubscription sub = new MotherSubscription();
            string latestItemToCompare = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                int bestMatch = 3;
                transactionName = transactionName.Replace("/", " ");
                transactionName = Regex.Replace(transactionName, @"[^a-zA-Z0-9 -.]", "");

                transactionName = transactionName.Replace("-", string.Empty);
                transactionName = transactionName.Replace("*", " * ");
                transactionName = transactionName.Replace("  ", string.Empty);
                transactionName = transactionName.ToLower();
                transactionName = transactionName.Replace("recurring", string.Empty);
                transactionName = transactionName.Replace(" on ", string.Empty);
                transactionName = transactionName.Replace("xxxxxxxxx", string.Empty);
                transactionName = transactionName.Replace("db.tt", string.Empty);
                transactionName = transactionName.Replace("cchelp", string.Empty);
                transactionName = transactionName.Replace("bill pay", string.Empty);
                transactionName = transactionName.Replace("payment", string.Empty).ToLower();
                transactionName = transactionName.Trim();
                transactionName = Regex.Replace(transactionName, @"\d+$", "");


                using (SqlCommand command = new SqlCommand("SELECT * FROM [Beam].dbo.MotherSubscriptions_BilledAs", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int higher = 0;
                        string itemToCompare = reader["Name"].ToString();

                        itemToCompare = itemToCompare.Replace("/", " ");
                        itemToCompare = Regex.Replace(itemToCompare, @"[^a-zA-Z0-9 -.]", "");
                        itemToCompare = itemToCompare.Replace("-", string.Empty);
                        itemToCompare = itemToCompare.Replace("*", " * ");
                        itemToCompare = itemToCompare.Replace("  ", string.Empty);
                        itemToCompare = itemToCompare.ToLower();
                        itemToCompare = itemToCompare.Replace("recurring", string.Empty);
                        itemToCompare = itemToCompare.Replace(" on ", string.Empty);
                        itemToCompare = itemToCompare.Replace("xxxxxxxxx", string.Empty);
                        itemToCompare = itemToCompare.Replace("db.tt", string.Empty);
                        itemToCompare = itemToCompare.Replace("cchelp", string.Empty);
                        itemToCompare = itemToCompare.Replace("bill pay", string.Empty);
                        itemToCompare = itemToCompare.Replace("payment", string.Empty).ToLower();
                        itemToCompare = itemToCompare.Trim();
                        itemToCompare = Regex.Replace(itemToCompare, @"\d+$", "");
                        int algoInt = StringIndex(transactionName, itemToCompare);
                        int algoIntOpp = StringIndex(itemToCompare, transactionName);
                        if (algoInt >= algoIntOpp)
                        {
                            higher = algoInt;
                        }
                        else
                        {
                            higher = algoIntOpp;
                        }
                        if (higher > bestMatch)
                        {
                            bestMatch = higher;
                            sub.Id = Convert.ToInt32(reader["Id"].ToString());
                            sub.MotherSubscriptionId = Convert.ToInt32(reader["MotherSubscriptionId"].ToString());
                            sub.Name = reader["Name"].ToString();
                            latestItemToCompare = itemToCompare;
                        }
                    }
                }
            }

            // now check both have words containing each other
            if (sub.Name != null)
            {
                string[] same = latestItemToCompare.ToLower().Split(' ').Intersect(transactionName.Split(' ')).ToArray();
                string[] same1 = transactionName.Split(' ').Intersect(latestItemToCompare.ToLower().Split(' ')).ToArray();
                if (same.Count() > 0 || same1.Count() > 0)
                {
                    if (sub.MotherSubscriptionId != 0)
                    {
                        //get shortest item as the name
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            con.Open();

                            using (SqlCommand command = new SqlCommand("select * from MotherSubscriptions where Id = " + sub.MotherSubscriptionId, con))
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    sub.Id = Convert.ToInt32(reader["Id"]);
                                    sub.Name = reader["Name"].ToString();
                                    sub.LogoUrl = reader["LogoUrl"].ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    sub = null;
                }
            }
            return sub;
        }

        private static int FindMatch(string text, string pattern)
        {
            var total = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                var max = 0;
                for (int j = 4; j <= pattern.Length - i; j++)
                {
                    var temp = pattern.Substring(i, j);
                    if (text.Contains(temp))
                        if (max < temp.Length)
                            max = temp.Length;
                }
                total += max;
                if (max > 0)
                    i += max - 1;
            }
            return total;
        }

        private static int StringIndex(string pattern, string test)
        {
            var patternSplit = pattern.Split(' ');
            var testSplit = test.Split(' ');
            List<int> jS = new List<int>();
            var index = 0;
            for (var i = 0; i < testSplit.Length; i++)
            {
                for (var j = 0; j < patternSplit.Length; j++)
                {
                    if (testSplit[i] == patternSplit[j])
                    {
                        index += patternSplit[j].Length;
                        if (((i == 0) && (j == patternSplit.Length - 1)) || ((i == testSplit.Length - 1) && (j == 0))) index += 0;
                        else if ((i == j) && ((i != 0) || (i != testSplit.Length - 1)) && (j != 0) &&
                                 (j != testSplit.Length - 1))
                        {
                            index += 2;
                            jS.Add(j);
                        }
                        else
                        {
                            index += 1;
                            jS.Add(j);
                        }
                    }
                    else if (testSplit[i].StartsWith(patternSplit[j], StringComparison.Ordinal))
                    {
                        index += patternSplit[j].Length;
                        if (((i == 0) && (j == patternSplit.Length - 1)) || ((i == testSplit.Length - 1) && (j == 0))) index += 0;
                        else if ((i == j) && ((i != 0) || (i != testSplit.Length - 1)) && (j != 0) &&
                                 (j != testSplit.Length - 1))
                        {
                            index += 1;
                            jS.Add(j);
                        }
                        else index += 0;
                    }
                }
            }
            return Convert.ToInt32((index - neighborhoodCount(jS)).ToString());

        }
        private static int neighborhoodCount(List<int> jS)
        {
            int count = 0;
            for (int i = 0; i < jS.Count; i++)
            {
                for (int j = i + 1; j < jS.Count; j++)
                {
                    if (Math.Abs(jS[i] - jS[j]) == 1) count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        private static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
        public static UserSubscription GetUserSubscriptionForTransaction(string databaseName, string transactionName, int accountId)
        {
            //get mothersubscriptionid if exists
            int motherSubscriptionId = 0;
            string motherSubscription = string.Empty;
            string motherSubscriptionLogoUrl = string.Empty;

            //check exact first
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS INNER JOIN [Beam].dbo.MotherSubscriptions_BilledAs MSBA ON MS.Id=MSBA.MotherSubscriptionId WHERE MSBA.Name = '" + transactionName + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        motherSubscriptionId = Convert.ToInt32(reader["Id"].ToString());
                        motherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                        motherSubscription = reader["Name"].ToString();
                    }
                }
            }

            //no exact match
            if (motherSubscriptionId == 0)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("SELECT MS.Id, MS.LogoUrl, MS.Name FROM [Beam].dbo.MotherSubscriptions MS INNER JOIN [Beam].dbo.MotherSubscriptions_BilledAs MSBA ON MS.Id=MSBA.MotherSubscriptionId WHERE MSBA.Name LIKE '%" + transactionName + "%'", con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int levenDistance = 10000;
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                            //this could bring back multiple results - need to find best match
                            int currentLeven = ComputeLevenshteinDistance(transactionName.ToUpper(), reader["Name"].ToString().ToUpper());
                            if (currentLeven < levenDistance)
                            {
                                levenDistance = currentLeven;
                                motherSubscriptionId = Convert.ToInt32(reader["Id"].ToString());
                                motherSubscriptionLogoUrl = reader["LogoUrl"].ToString();
                                motherSubscription = reader["Name"].ToString();
                            }

                        }

                        if (motherSubscriptionId == 0)
                        {
                            //TODO: it didnt bring back a result, however the transaction name could still be a mothersubs disguised. Eg. Justins one:
                            //HLU*Hulu 3183129-U HULU.COM/BILLCA
                            MotherSubscription sub = GetMotherSubForTransactionName(transactionName);
                            if (sub != null)
                            {
                                motherSubscriptionId = sub.Id;
                                motherSubscriptionLogoUrl = sub.LogoUrl;
                                motherSubscription = sub.Name;
                            }
                        }
                    }

                }
            }

            if (motherSubscriptionId != 0)
            {
                //it exists as mother subs - does the user have it as a legit subscription of their own?
                using (Beam db = new Beam(connectionStringWithUser(databaseName)))
                {
                    var item = db.UserSubscription.Where(m => m.motherSubscriptionId == motherSubscriptionId).ToList();
                    if (item != null && item.Count() > 0)
                    {
                        //yes they have this subs. return it 
                        return item.First();
                    }
                    else
                    {
                        //no they don't. and its confirmed as a sub. create it
                        UserSubscription newUserSub = new UserSubscription();
                        newUserSub.motherSubscriptionId = motherSubscriptionId;
                        newUserSub.Name = motherSubscription;
                        newUserSub.IsActive = true;
                        newUserSub.accountid = accountId;

                        newUserSub = db.UserSubscription.Add(newUserSub);
                        db.SaveChanges();

                        return newUserSub;
                    }
                }
            }
            else
            {
                using (Beam db = new Beam(connectionStringWithUser(databaseName)))
                {
                    //first check they don't have the sub already
                    var existing = db.UserSubscription.Where(d => d.Name == transactionName);

                    if (existing != null && existing.Count() > 0)
                    {
                        return existing.First();
                    }
                    else
                    {
                        // create a user sub but just set it as a recurring payment, not a sub
                        UserSubscription newUserSub = new UserSubscription();
                        newUserSub.motherSubscriptionId = 0;
                        newUserSub.Name = transactionName;
                        newUserSub.IsActive = true;
                        newUserSub.accountid = accountId;

                        newUserSub = db.UserSubscription.Add(newUserSub);
                        db.SaveChanges();
                        return newUserSub;
                    }


                }
            }

        }
    }
}
