using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Net;
using Plaid.Net;
using Saastrack.DAL.Operations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Plaid.Net.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace GetTransactionsFromPlaid
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        private static Int64 GetTime()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        static void Main()
        {
            MainAsync().Wait();
        }

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static async Task MainAsync()
        {
            //go through each db
            //get all accounts
            //foreach account
            //get all transactions and push to transaction db
            //store last transaction date stored

            Console.WriteLine("Transactions started");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new HttpPlaidClient(new Uri("https://tartan.plaid.com"), "57cf6a22cfbf49f67b01fd7c", "ddab34c3ae74812f084dd49e34b7c6");

            Guid databaseGuid = Guid.Parse(UserDetailOperations.GetNextDbNameForTransactionsFromPlaid());

            Console.WriteLine("Database: {0}", databaseGuid.ToString());
            var account = CloudStorageAccount.Parse(GetConnectionString());
            // You could use local development storage 
            //   account = CloudStorageAccount.DevelopmentStorageAccount; 
            // Create the table client.     
            CloudTableClient tableClient = account.CreateCloudTableClient();
            // Create the table if it doesn't exist. 
            CloudTable table = tableClient.GetTableReference("transactions");
            string userDb = databaseGuid.ToString();
            string accessToken = CardOperations.GetAccessTokenForUser(userDb);

            try
            {
                if (accessToken != string.Empty)
                {

                    Console.WriteLine("Access Token Found - try accounts");
                    //get each account for user - this is the partitionkey
                    List<Saastrack.DAL.Account> listOfAccounts = AccountOperations.GetAllAccountsForUser(userDb);
                    foreach (Saastrack.DAL.Account accountItem in listOfAccounts)
                    {
                        //get all transactions only for 3 days before the last added - that way we never miss any
                        //var lastAddedTrans = accountItem.lastdateadded.AddYears(-5);
                        //var dateString = lastAddedTrans.Year + "-" + lastAddedTrans.Month + "-" + lastAddedTrans.Day + "T00:00:00Z";
                        var lastAdded = DateTimeOffset.Parse("01/01/1990");
                        Console.WriteLine("user db:" + userDb + " - " + accountItem._id);
                        TransactionResult transactions = await client.GetTransactionsAsync(new AccessToken(accessToken), null, accountItem._id, lastAdded);
                        DateTime lastUpdated = new DateTime();
                        if (transactions.Transactions != null)
                        {
                            var transactionList = transactions.Transactions.OrderByDescending(d => d.Date);
                            foreach (Plaid.Net.Models.Transaction trans in transactionList)
                            {
                                string meta_location_address = string.Empty;
                                if (trans.Metadata.location.address != null)
                                {
                                    meta_location_address = trans.Metadata.location.address;
                                }

                                string meta_location_city = string.Empty;
                                if (!string.IsNullOrEmpty(trans.Location.City))
                                {
                                    meta_location_city = trans.Location.City;
                                }

                                string meta_location_state = string.Empty;
                                if (!string.IsNullOrEmpty(trans.Location.State))
                                {
                                    meta_location_state = trans.Location.State;
                                }

                                string meta_location_street = string.Empty;
                                if (!string.IsNullOrEmpty(trans.Location.Street))
                                {
                                    meta_location_street = trans.Location.Street;
                                }

                                float meta_location_coordinates_lat = 0;
                                if (trans.Location.Latitude.HasValue)
                                {
                                    meta_location_coordinates_lat = trans.Location.Latitude.Value;
                                }

                                float meta_location_coordinates_lon = 0;
                                if (trans.Location.Longitude.HasValue)
                                {
                                    meta_location_coordinates_lon = trans.Location.Longitude.Value;
                                }

                                var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Minutes;

                                double score_location_address = 0;
                                if (trans.Score.location.address != null)
                                {
                                    score_location_address = trans.Score.location.address;
                                }

                                double score_location_city = 0;
                                if (trans.Score.location.city != null)
                                {
                                    score_location_city = trans.Score.location.city;
                                }

                                double score_location_state = 0;
                                if (trans.Score.location.state != null)
                                {
                                    score_location_state = trans.Score.location.state;
                                }

                                double score_name = 0;
                                if (trans.Score.name != null)
                                {
                                    score_name = trans.Score.name;
                                }

                                string trans_type = string.Empty;
                                if (trans.Type != null)
                                {
                                    trans_type = trans.Type.primary;
                                }

                                string categories = string.Empty;
                                if (trans.Categories != null)
                                {
                                    categories = string.Join(",", trans.Categories);
                                }

                                string meta = string.Empty;
                                if (trans.Metadata != null)
                                {
                                    meta = JsonConvert.SerializeObject(trans.Metadata);
                                }

                                string score = string.Empty;
                                if (trans.Score != null)
                                {
                                    score = JsonConvert.SerializeObject(trans.Metadata);
                                }

                                string typeString = string.Empty;
                                if (trans.Score != null)
                                {
                                    typeString = JsonConvert.SerializeObject(trans.Score);
                                }

                                lastUpdated = trans.Date.Date;

                                TransactionOperations.AddNewTransaction(trans.AccountId,
                                    trans.TransactionId, GetTime().ToString(), string.Empty, string.Empty,
                                    trans.Amount, categories, trans.CategoryId, trans.Date, meta, meta_location_address, meta_location_city, meta_location_coordinates_lat, meta_location_coordinates_lon,
                                    meta_location_state, trans.Name, offset.ToString(), trans.IsPending, score, score_location_address, score_location_city, score_location_state, score_name, typeString, trans_type);
                            }
                        }

                        //update last added date - this is wrong
                        AccountOperations.AddLastAddedDateForAccount(userDb, lastUpdated, accountItem.Id);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Error: " + ex.StackTrace);
            }
            finally
            {
                UserDetailOperations.UpdatePlaidLastProcessedAndLocked(userDb);                
            }
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
