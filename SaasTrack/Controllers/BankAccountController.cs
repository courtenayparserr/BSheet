
using log4net;
using Newtonsoft.Json;
using Plaid.Net;
using Plaid.Net.Models;
using Saastrack.DAL;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SaasTrack.Controllers
{
    public class BankAccountController : ApiController
    {
        private Int64 GetTime()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        // GET api/<controller>/5
        [Route("api/bankaccount/getuser")]
        public bool GetUser(string id)
        {
            return true;
        }

        // GET api/<controller>/5
        public async Task<string> Get(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var logger = LogManager.GetLogger(typeof(BankAccountController));
            //Send plaintext
            logger.Info("Received request from Plaid Link for:" + id);


            try
            {
                string[] idSplit = id.Split(';');
                var client = new HttpPlaidClient(new Uri("https://tartan.plaid.com"), "57cf6a22cfbf49f67b01fd7c", "ddab34c3ae74812f084dd49e34b7c6");
                var accessToken = await client.ExchangeBankTokenWithOutAsync(idSplit[0]);
                logger.Info("Got access token:" + accessToken.AccessToken.Value);

                //add access token to DB for later usage
                Card newCard = CardOperations.InsertInitialCardData(idSplit[1], accessToken.AccessToken.Value, idSplit[0]);
                logger.Info("Inserted card");
                var accounts = await client.GetAccountsAsync(new AccessToken(accessToken.AccessToken.Value));
                logger.Info("got accounts");
                
                foreach(Plaid.Net.Models.Account acc in accounts.Value)
                {
                    Saastrack.DAL.Account newAcc = new Saastrack.DAL.Account();
                    newAcc.lastdateprocessed = Convert.ToDateTime("01/01/1990");
                    newAcc.lastdateadded = Convert.ToDateTime("01/01/1990");
                    newAcc.enabled = true;
                    newAcc._id = acc.Id;
                    newAcc._item = acc.ItemId;
                    newAcc.balance_available = acc.AvailableBalance.ToString();
                    newAcc.balance_current = acc.CurrentBalance.ToString();
                    string accName = string.Empty;
                    acc.Metadata.TryGetValue("name", out accName);
                    newAcc.meta_name = accName;

                    string accnumber = string.Empty;
                    acc.Metadata.TryGetValue("number", out accnumber);
                    newAcc.meta_number = accnumber;

                    newAcc.institution_type = acc.InstitutionType.Value;
                    if(acc.AccountType != null)
                        newAcc.type = acc.AccountType.Value;

                    if (acc.AccountSubtype != null)
                        newAcc.subtype = acc.AccountSubtype.Value;

                    AccountOperations.InsertAccount(idSplit[1], newAcc, newCard.Id);
                    logger.Info("added account: " + acc.Id);

                    TransactionResult transactions = await client.GetTransactionsAsync(new AccessToken(accessToken.AccessToken.Value), null, newAcc._id);
                    logger.Info("got transactions");
                    if (transactions.Transactions != null)
                    {
                        foreach (Plaid.Net.Models.Transaction trans in transactions.Transactions)
                        {
                            logger.Info("Processing trans " + trans.TransactionId);
                            string meta_location_address = string.Empty;
                            if (trans.Metadata.location.address != null)
                            {
                                meta_location_address = trans.Metadata.location.address;
                            }
                            logger.Info("Address done");

                            string meta_location_city = string.Empty;
                            if (!string.IsNullOrEmpty(trans.Location.City))
                            {
                                meta_location_city = trans.Location.City;
                            }
                            logger.Info("City done");

                            string meta_location_state = string.Empty;
                            if (!string.IsNullOrEmpty(trans.Location.State))
                            {
                                meta_location_state = trans.Location.State;
                            }
                            logger.Info("State done");

                            string meta_location_street = string.Empty;
                            if (!string.IsNullOrEmpty(trans.Location.Street))
                            {
                                meta_location_street = trans.Location.Street;
                            }
                            logger.Info("Street done");

                            float meta_location_coordinates_lat = 0;
                            if (trans.Location.Latitude.HasValue)
                            {
                                meta_location_coordinates_lat = trans.Location.Latitude.Value;
                            }
                            logger.Info("Lat done");

                            float meta_location_coordinates_lon = 0;
                            if (trans.Location.Longitude.HasValue)
                            {
                                meta_location_coordinates_lon = trans.Location.Longitude.Value;
                            }
                            logger.Info("Lon done");

                            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Minutes;

                            double score_location_address = 0;
                            if (trans.Score.location.address != null)
                            {
                                score_location_address = trans.Score.location.address;
                            }
                            logger.Info("address done");

                            double score_location_city = 0;
                            if (trans.Score.location.city != null)
                            {
                                score_location_city = trans.Score.location.city;
                            }
                            logger.Info("city done");

                            double score_location_state = 0;
                            if (trans.Score.location.state != null)
                            {
                                score_location_state = trans.Score.location.state;
                            }
                            logger.Info("State done");

                            double score_name = 0;
                            if (trans.Score.name != null)
                            {
                                score_name = trans.Score.name;
                            }
                            logger.Info("Score name done");

                            string trans_type = string.Empty;
                            if (trans.Type != null)
                            {
                                trans_type = trans.Type.primary;
                            }
                            logger.Info("trans.Type done");

                            string categories = string.Empty;
                            if (trans.Categories != null)
                            {
                                categories = string.Join(",", trans.Categories);
                            }
                            logger.Info("Categories done");

                            string meta = string.Empty;
                            if (trans.Metadata != null)
                            {
                                meta = JsonConvert.SerializeObject(trans.Metadata);
                            }
                            logger.Info("Meta done");

                            string score = string.Empty;
                            if (trans.Score != null)
                            {
                                score = JsonConvert.SerializeObject(trans.Metadata);
                            }
                            logger.Info("Score done");

                            string typeString = string.Empty;
                            if (trans.Score != null)
                            {
                                typeString = JsonConvert.SerializeObject(trans.Score);
                            }
                            logger.Info("Type done");

                            TransactionOperations.AddNewTransaction(trans.AccountId,
                                trans.TransactionId, GetTime().ToString(), string.Empty, string.Empty,
                                trans.Amount, categories, trans.CategoryId, trans.Date, meta, meta_location_address, meta_location_city, meta_location_coordinates_lat, meta_location_coordinates_lon,
                                meta_location_state, trans.Name, offset.ToString(), trans.IsPending, score, score_location_address, score_location_city, score_location_state, score_name, typeString, trans_type);
                        }
                    }

                }
                //var transactions = await client.GetTransactionsAsync(new AccessToken(accessToken.AccessToken.Value));

                return "value";
            }
            catch (Exception ex)
            {
                //Send an exception
                logger.Error("error in bankaccountcontroller.addbankaccount", ex);
                return "didnt work";
            }
            ////client.ExchangeToken()
        }

    }
}
