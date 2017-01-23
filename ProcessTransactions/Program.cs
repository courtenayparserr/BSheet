using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Saastrack.DAL;
using Saastrack.DAL.Operations;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaasTrackProcessTransactions
{
    class Program
    {
        static void Main()
        {
            string userDb = string.Empty;
            try
            {
                string[] disclNames = AccountOperations.GetDiscludedWords().ToArray();

                List<string> inclNamesList = AccountOperations.GetIncludedWords();
                inclNamesList.Add("Recurring");
                inclNamesList.Add("digitalocean.com");
                string[] inclNames = inclNamesList.ToArray();
                Console.WriteLine("Transactions started");
                //this should change to simply go get the next one which hasnt been processed - then set the web job to run continuously
                Guid databaseGuid = Guid.Parse(UserDetailOperations.GetNextDbName());
                userDb = databaseGuid.ToString();
                UserDetailOperations.LockDb(databaseGuid.ToString());
                //go get the details again - they may have changed
                Console.WriteLine("Database: {0}", databaseGuid.ToString());
                var account = CloudStorageAccount.Parse(GetConnectionString());
                // You could use local development storage 
                //   account = CloudStorageAccount.DevelopmentStorageAccount; 
                // Create the table client.     
                CloudTableClient tableClient = account.CreateCloudTableClient();

                // Create the table if it doesn't exist. 
                CloudTable table = tableClient.GetTableReference("transactions");

                //get each account for user - this is the partitionkey
                List<Account> listOfAccounts = AccountOperations.GetAllAccountsForUser(userDb);
                foreach (Account accountItem in listOfAccounts)
                {
                    Console.WriteLine("Database: {0}; Account {1}", databaseGuid.ToString(), accountItem.meta_name);
                    string partitionKey = accountItem._id;
                    var exQuery =
                    new TableQuery<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                                                                                    partitionKey));
                    if (exQuery != null)
                    {
                        var listOfQuoteEntities = table.ExecuteQuery(exQuery).Select(ent => (Saastrack.DAL.Operations.TransactionOperations.QuoteEntity)ent).ToList();

                        var listOfQuoteFOrDateTimeOffset = (from specificTopicList in listOfQuoteEntities
                                                            orderby specificTopicList.date descending
                                                            select specificTopicList);

                        //Create a datatable of items
                        DataTable dt = CreateDataTableFromQuoteEntities(listOfQuoteFOrDateTimeOffset.ToList());
                        try
                        {
                            List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>> listOfLists = ProcessHelper.GetGroupsOfViableTransactions(dt, disclNames, inclNames);
                            //pretend theyre in order
                            //do one group at a time
                            foreach (List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity> listEntity in listOfLists)
                            {

                                bool proceed = true;
                                bool proceedIncl = false;

                                if (listEntity.Count() == 2)
                                {
                                    if (GetPercentageDifferenceBtnTwoPrices(listEntity[0].amount, listEntity[1].amount) > 10)
                                    {
                                        proceed = false;
                                    }
                                }

                                if (listEntity[0].amount < 0)
                                {
                                    proceed = false;
                                }

                                //if its in incl names it should go ahead
                                proceedIncl = inclNames.Any(s => s.ToLower().Equals(listEntity[0].name.ToLower()));
                                if (proceed || proceedIncl)
                                {
                                    try
                                    {
                                        UserSubscription sub = new UserSubscription();
                                        int i = 0;
                                        foreach (Saastrack.DAL.Operations.TransactionOperations.QuoteEntity transaction in listEntity)
                                        {
                                            if (i == 0)
                                            {
                                                sub = UserSubscriptionOperations.GetUserSubscriptionForTransaction(userDb, transaction.name, accountItem.Id);
                                            }

                                            //first item
                                            UserSubscriptionPayment payment = new UserSubscriptionPayment();
                                            payment.Amount = transaction.amount;
                                            payment.Date = transaction.date.DateTime;
                                            payment.TransactionId = transaction.RowKey;

                                            UserSubscriptionOperations.AddPaymentToUserSubscription(userDb, payment, sub.Id);
                                            i++;
                                        }

                                        //update datecreated for subscription
                                        var firstPayment = UserSubscriptionOperations.GetFirstPaymentForSubscription(userDb, sub.Id);
                                        if (firstPayment != null)
                                        {
                                            UserSubscriptionOperations.UpdateDateCreatedForSubscription(userDb, sub.Id, firstPayment.Date);
                                        }

                                        //update previous billing - as in last billed
                                        UserSubscriptionOperations.UpdatePreviousBillingForSubscription(userDb, sub.Id);
                                        //update period
                                        UserSubscriptionOperations.UpdatePeriodForSubscription(userDb, sub.Id);
                                        //update period
                                        UserSubscriptionOperations.UpdateIsActiveForSubscription(userDb, sub.Id);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(string.Format("Error processing transaction {0} in account {1} for DB {2}", listEntity[0].name, accountItem._id, userDb));
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(string.Format("Error processing account {0} for DB {1}", accountItem._id, userDb));
                        }

                        //now update their account dashboard
                        double totalAccMonthly = UserSubscriptionOperations.GetTotalValueForSubscriptionForDatesAndAccount(userDb, DateTime.Now.AddDays(-30), DateTime.Now, accountItem);
                        double totalAccAnnual = UserSubscriptionOperations.GetTotalValueForSubscriptionForDatesAndAccount(userDb, DateTime.Now.AddDays(-365), DateTime.Now, accountItem);

                        double totalAccLastMonth = UserSubscriptionOperations.GetTotalValueForSubscriptionForDatesAndAccount(userDb, DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-30), accountItem);
                        double totalAccLastAnnual = UserSubscriptionOperations.GetTotalValueForSubscriptionForDatesAndAccount(userDb, DateTime.Now.AddDays(-730), DateTime.Now.AddDays(-365), accountItem);

                        double monthlyAccIncreaseDecrease = totalAccMonthly - totalAccLastMonth;
                        double annualAccIncreaseDecrease = totalAccAnnual - totalAccLastAnnual;
                        //TODO: update total subs

                        int totalSubs1 = 0;
                        totalSubs1 = UserSubscriptionOperations.GetActiveSubscriptions(userDb).Where(m => m.accountid == accountItem.Id).Count();
                        //update the account dashboard
                        accountItem.Monthly = totalAccMonthly;
                        accountItem.MonthlyIncreaseDecrease = monthlyAccIncreaseDecrease;
                        accountItem.Annual = totalAccAnnual;
                        accountItem.AnnualIncreaseDecrease = annualAccIncreaseDecrease;
                        accountItem.Subs = totalSubs1;
                        AccountOperations.UpdateDashboardForAccount(userDb, accountItem);

                    }
                }

                //now update their dashboard
                double totalMonthly = UserSubscriptionOperations.GetTotalValueForSubscriptionForDates(userDb, DateTime.Now.AddDays(-30), DateTime.Now);
                double totalAnnual = UserSubscriptionOperations.GetTotalValueForSubscriptionForDates(userDb, DateTime.Now.AddDays(-365), DateTime.Now);

                double totalLastMonth = UserSubscriptionOperations.GetTotalValueForSubscriptionForDates(userDb, DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-30));
                double totalLastAnnual = UserSubscriptionOperations.GetTotalValueForSubscriptionForDates(userDb, DateTime.Now.AddDays(-730), DateTime.Now.AddDays(-365));

                double monthlyIncreaseDecrease = totalMonthly - totalLastMonth;
                double annualIncreaseDecrease = totalAnnual - totalLastAnnual;

                //TODO: update total subs

                int totalSubs = 0;
                totalSubs = UserSubscriptionOperations.GetActiveSubscriptions(userDb).Count();

                //TODO: need to get inactive seats (done) + employee engagement(done) + potential savings 
                int inactiveTotalSeats = 0;
                TimeSpan totalEmployeeEngagement = new TimeSpan();
                var servicesList = ServiceOperations.GetCompanyUserServicesForCompany(userDb);
                foreach (CompanyUserServices companyServ in servicesList)
                {
                    totalEmployeeEngagement += ServiceOperations.GetTotalTimeSpentForService(userDb, companyServ.serviceid.Value);
                    int numberOfDaysForService = 0;
                    if (!companyServ.service.InactiveWhenDays.HasValue)
                    {
                        numberOfDaysForService = 30;
                    }
                    else
                    {
                        numberOfDaysForService = companyServ.service.InactiveWhenDays.Value;
                    }

                    TimeSpan timeSpan = DateTime.Now - companyServ.LastLogin;

                    if (timeSpan.Days > numberOfDaysForService)
                    {
                        //its an inactive seat for that service
                        inactiveTotalSeats++;
                    }
                }

                UserDashboard newDash = new UserDashboard();
                newDash.Annual = totalAnnual;
                newDash.Monthly = totalMonthly;
                newDash.MonthlyIncreaseDecrease = monthlyIncreaseDecrease;
                newDash.AnnualIncreaseDecrease = annualIncreaseDecrease;
                newDash.Subs = totalSubs;
                newDash.InactiveSeats = inactiveTotalSeats;
                newDash.EmployeeEngagement = totalEmployeeEngagement.Hours + "hrs" + totalEmployeeEngagement.Minutes + "mins";

                UserDashboardOperations.UpdateUserDashboard(userDb, newDash);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                UserDetailOperations.UpdateLastProcessedAndLocked(userDb);
            }

        }

        private static double GetPercentageDifferenceBtnTwoPrices(double price1, double price2)
        {
            return (Math.Abs(price1 - price2) / ((price1 + price2) / 2)) * 100;
        }

        private static DataTable CreateDataTableFromQuoteEntities(List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity> listOfQuoteEntities)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Name");
            dt.Columns.Add("TransactionId");
            foreach (Saastrack.DAL.Operations.TransactionOperations.QuoteEntity transaction in listOfQuoteEntities)
            {
                DataRow row = dt.NewRow();
                row["Date"] = transaction.date;
                row["Amount"] = transaction.amount;
                row["Name"] = transaction.name;
                row["TransactionId"] = transaction.RowKey;
                dt.Rows.Add(row);
            }
            return dt;
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
