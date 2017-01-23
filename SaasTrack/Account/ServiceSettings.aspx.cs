using Saastrack.DAL;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaasTrack.Account
{
    public partial class ServiceSettings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    Service theService = ServiceOperations.GetService(DatabaseName, id);
                    serviceName.Text = theService.MasterServiceName;
                    serviceName2.Text = theService.MasterServiceName;
                    serviceName3.Text = theService.MasterServiceName;
                    serviceName4.Text = theService.MasterServiceName;
                    serviceName5.Text = theService.MasterServiceName;
                    string textText = string.Empty;

                    // hide urls if it is an app eg: slack
                    if(!theService.MasterServiceIsApp)
                    {
                        foreach (ServiceUrl servUrl in theService.serviceurls)
                        {
                            textText += servUrl.Url + Environment.NewLine;
                        }
                        serviceUrls.Text = textText;
                    }
                    else
                    {
                        serviceUrls.Visible = false;
                        serviceUrlsValues.Visible = false;
                        serviceUrlsLabel.Visible = false;
                    }
                    
                    if (theService.AddedByBankFeed.HasValue && theService.AddedByBankFeed.Value)
                    {
                        RadNumericTextBox2.Enabled = false;
                    }
                    if (theService.InactiveWhenDays.HasValue)
                        RadNumericTextBox1.Value = theService.InactiveWhenDays.Value;
                }
                
            }
        }

        public static string GetBaseDomain(string domainName)
        {
            var tokens = domainName.Split('.');

            // only split 3 segments like www.west-wind.com
            if (tokens == null || tokens.Length != 3)
                return domainName;

            var tok = new List<string>(tokens);
            var remove = tokens.Length - 2;
            tok.RemoveRange(0, remove);

            return tok[0] + "." + tok[1]; ;
        }

        protected void saveSettings_Click(object sender, EventArgs e)
        {
            string urls = serviceUrls.Text;
            string[] lst = urls.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Service theService = ServiceOperations.GetService(DatabaseName, id);
            theService.serviceurls = new List<ServiceUrl>();
            bool hasWWWVersion = false;
            bool continueP = true;
            foreach(string item in lst)
            {
                //TODO: need to addin a www version too??
                Uri uriResult;
                bool result = Uri.TryCreate(item, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if(result)
                {
                    ServiceUrl urlS = new ServiceUrl();
                    if(uriResult.Host.ToString().ToLower().StartsWith("www."))
                    {
                        hasWWWVersion = true;
                    }
                    urlS.Url = uriResult.ToString();
                    theService.serviceurls.Add(urlS);
                }
                else
                {
                    continueP = false;
                    errorPanel.Visible = true;
                    errorMessage.Text = "One of your Service URL's are incorrect. Please try again";
                }                
            }

            if (continueP)
            {
                if (RadNumericTextBox1.Value.HasValue)
                    theService.InactiveWhenDays = (int)RadNumericTextBox1.Value.Value;
                ServiceOperations.UpdateServiceUrlsAndService(DatabaseName, theService);
                Response.Redirect("/account/services");
            }
        }
    }
}