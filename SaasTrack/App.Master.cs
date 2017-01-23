using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Saastrack.DAL;
using Saastrack.DAL.Operations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaasTrack
{
    public partial class AppMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            accountHtml.Text = GetAccountsMenuHTML();
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
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        public string GetAccountsMenuHTML()
        {
            string returnHtml = "<div class='panel-group' id='accordion'><a data-parent='#accordion'>Accounts</a>";
            List<Saastrack.DAL.Account> accounts = AccountOperations.GetAllAccountsForUserInInstitutionOrder(DatabaseName);
            string instName = string.Empty;
            foreach(var account in accounts)
            {
                if(account.institution_type != instName)
                {
                    //start or new -- need to close old one if need be
                    if(returnHtml.Contains("table"))
                    {
                        // need to close old one
                        returnHtml += "</table></div></div></div>";
                    }
                    returnHtml += "<div class=\"panel panel-default\"><div class=\"panel-heading\">";
                    returnHtml += "<a data-toggle='collapse' data-parent='#accordion' href='#" + account.institution_type  +"One'>" + account.institution_type + "</a></div>";
                    returnHtml += "<div id='" + account.institution_type + "One' class='panel-collapse collapse in'><div class='panel-body'><table class='table'>";
                    returnHtml += "<tr><td><a href=\"#\">" + account.meta_name + "</a></td></tr>";
                }
                else
                {
                    returnHtml += "<tr><td><a href=\"#\">" + account.meta_name + "</a></td></tr>";
                }

                instName = account.institution_type;
            }
            returnHtml += "</table></div></div></div>";
            returnHtml += "</div>";
            return returnHtml;

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

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
    }

}