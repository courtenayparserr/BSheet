using Newtonsoft.Json;
using Plaid.Net;
using Plaid.Net.Models;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaasTrack
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Test();
        }

        public async void Test()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

              try
              {
                  var client = new HttpPlaidClient(new Uri("https://tartan.plaid.com"), "57cf6a22cfbf49f67b01fd7c", "ddab34c3ae74812f084dd49e34b7c6");
                  var accounts = await client.GetAccountsAsync(new AccessToken("2d2d3b738ce1efb43b4218a2ba4fb5cb36e97c506c19bbfff564dc6939fa8a3bbed384794027b6f81471ba9e1c753a4dd5bb57209cb28b711e87e42719a7cbb8b577a5fb54f9593967a67c45e6af4a53"));

                  foreach (Plaid.Net.Models.Account acc in accounts.Value)
                  {
                      Saastrack.DAL.Account newAcc = new Saastrack.DAL.Account();
                      newAcc.lastdateprocessed = DateTime.Today.AddDays(-7.0);
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
                      if (acc.AccountType != null)
                          newAcc.type = acc.AccountType.Value;

                      if (acc.AccountSubtype != null)
                          newAcc.subtype = acc.AccountSubtype.Value;

                      newAcc.enabled = true;
                      AccountOperations.InsertAccount("323e3098-cc87-4b37-8eb5-85a6d6ddba1c", newAcc, 1);
                      
                      //TransactionResult transactions = await client.GetTransactionsAsync(new AccessToken("2d2d3b738ce1efb43b4218a2ba4fb5cb36e97c506c19bbfff564dc6939fa8a3bbed384794027b6f81471ba9e1c753a4dd5bb57209cb28b711e87e42719a7cbb8b577a5fb54f9593967a67c45e6af4a53"), null, newAcc._id);

                      //if (transactions.Transactions != null)
                      //{
                      //    foreach (Plaid.Net.Models.Transaction trans in transactions.Transactions)
                      //    {
                      //        string meta_location_address = string.Empty;
                      //        if (trans.Metadata.location.address != null)
                      //        {
                      //            meta_location_address = trans.Metadata.location.address;
                      //        }

                      //        string meta_location_city = string.Empty;
                      //        if (!string.IsNullOrEmpty(trans.Location.City))
                      //        {
                      //            meta_location_city = trans.Location.City;
                      //        }

                      //        string meta_location_state = string.Empty;
                      //        if (!string.IsNullOrEmpty(trans.Location.State))
                      //        {
                      //            meta_location_state = trans.Location.State;
                      //        }

                      //        string meta_location_street = string.Empty;
                      //        if (!string.IsNullOrEmpty(trans.Location.Street))
                      //        {
                      //            meta_location_street = trans.Location.Street;
                      //        }

                      //        float meta_location_coordinates_lat = 0;
                      //        if (trans.Location.Latitude.HasValue)
                      //        {
                      //            meta_location_coordinates_lat = trans.Location.Latitude.Value;
                      //        }

                      //        float meta_location_coordinates_lon = 0;
                      //        if (trans.Location.Longitude.HasValue)
                      //        {
                      //            meta_location_coordinates_lon = trans.Location.Longitude.Value;
                      //        }

                      //        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Minutes;

                      //        double score_location_address = 0;
                      //        if (trans.Score.location.address != null)
                      //        {
                      //            score_location_address = trans.Score.location.address;
                      //        }

                      //        double score_location_city = 0;
                      //        if (trans.Score.location.city != null)
                      //        {
                      //            score_location_city = trans.Score.location.city;
                      //        }

                      //        double score_location_state = 0;
                      //        if (trans.Score.location.state != null)
                      //        {
                      //            score_location_state = trans.Score.location.state;
                      //        }

                      //        double score_name = 0;
                      //        if (trans.Score.name != null)
                      //        {
                      //            score_name = trans.Score.name;
                      //        }

                      //        string trans_type = string.Empty;
                      //        if (trans.Type != null)
                      //        {
                      //            trans_type = trans.Type.primary;
                      //        }

                      //        string categories = string.Empty;
                      //        if (trans.Categories != null)
                      //        {
                      //            categories = string.Join(",", trans.Categories);
                      //        }

                      //        string meta = string.Empty;
                      //        if (trans.Metadata != null)
                      //        {
                      //            meta = JsonConvert.SerializeObject(trans.Metadata);
                      //        }

                      //        string score = string.Empty;
                      //        if (trans.Score != null)
                      //        {
                      //            score = JsonConvert.SerializeObject(trans.Metadata);
                      //        }

                      //        string typeString = string.Empty;
                      //        if (trans.Score != null)
                      //        {
                      //            typeString = JsonConvert.SerializeObject(trans.Score);
                      //        }


                      //    }
                      //}

                  }
              }
              catch (PlaidException e)
              {
                  // Use this exception to capture Plaid API errors 
                  // as specified in https://plaid.com/docs/#response-codes
                  // Error details wrapped in e.Error
                  // 
                  // if using WCF, you can easily use WebFaultException to handle the error
                  // throw new WebFaultException<Error>(e.Error, e.Error.StatusCode);
              }
              catch (Exception e)
              {

                  // Something else happened here
              }
        }
    }
}