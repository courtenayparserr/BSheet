using Microsoft.AspNet.Identity;
using Plaid.Net;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaasTrack
{
    public partial class Beam : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        public string DatabaseName
        {
            get
            {
                if (Session["databaseName"] != null && Session["databaseName"].ToString() != string.Empty)
                {
                    return Session["databaseName"].ToString();
                }
                else
                {
                    //need to change this to lookup instead
                    Session["databaseName"] = UserDetailOperations.GetUserDbName(HttpContext.Current.User.Identity.GetUserId());
                }
                if (string.IsNullOrEmpty(Session["databaseName"].ToString()))
                {
                    Context.GetOwinContext().Authentication.SignOut();
                    Response.Redirect("/Default.aspx");
                }
                return Session["databaseName"].ToString();
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;            
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    Response.Redirect("/Default.aspx"); 
                    //throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        public string GetMenuHTML()
        {
            string currentlySelected = Request.Url.PathAndQuery.ToLower();
            string isSelected = string.Empty;
            string dashboardLink = "<li><a href=\"/account/dashboard\">Dashboard</a></li>";
            string employeesLink = "<li><a href=\"/account/employees\">Employees</a></li>";
            string accountLink = "<li><a href=\"/account/AddBankAccount\">Accounts</a>";
            switch(currentlySelected)
            {
                case "/account/dashboard":
                    dashboardLink = "<li class='selected'><a href=\"/account/dashboard\">Dashboard</a></li>";
                    break;
                case "/account/employees":
                    employeesLink = "<li class='selected'><a href=\"/account/employees\">Employees</a></li>";
                    break;
                case "/account/addbankaccount":
                    accountLink = "<li class='selected'><a href=\"/account/AddBankAccount\">Accounts</a>";
                    break;
            }
            string returnHtml = dashboardLink + accountLink; //dashboardLink + employeesLink + accountLink;
            List<Saastrack.DAL.Account> accounts = AccountOperations.GetAllAccountsForUserInInstitutionOrder(DatabaseName);
            string instName = string.Empty;
            int i = 0;
            foreach (var account in accounts)
            {
                string newLink = "/account/dashboard?account=" + account.Id;
                if(newLink.ToLower() == currentlySelected)
                {
                    isSelected = " selected";
                }
                if (account.institution_type != instName)
                {
                    if(i != 0)
                    {
                        returnHtml += "</div>";
                    }
                    returnHtml += "<div class=\"account\"><i class=\"fa fa-arrow-right\" aria-hidden=\"true\"></i>";
                    returnHtml += "<a class=\"main-link\" href=\"#\">" + InstitutionOperations.GetInstitutionName(account.institution_type) + "</a>";                    
                    returnHtml += "<a class=\"sub-link" + isSelected + "\" href=\"" + newLink + "\">" + account.meta_name + "</a>";
                }
                else
                {
                    returnHtml += "<a class=\"sub-link" + isSelected + "\" href=\"" + newLink + "\">" + account.meta_name + "</a>";
                }

                instName = account.institution_type;
                isSelected = string.Empty;
                i++;
            }
            if(accounts != null && accounts.Count() > 0)
                returnHtml += "</div>";
            return returnHtml + "</li>";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                accountHtml.Text = GetMenuHTML();
            }
        }

        public string GetInstitutionName(string institutionType)
        {  
            
            return "Wells!";
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
        }
    }
}