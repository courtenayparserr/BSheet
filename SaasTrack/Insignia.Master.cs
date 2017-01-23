using Microsoft.AspNet.Identity;
using Plaid.Net;
using Saastrack.DAL;
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
    public partial class Insignia : System.Web.UI.MasterPage
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
            string currentlySelected = Request.Url.AbsolutePath.ToLower();
            string isSelected = string.Empty;
            string dashboardLink = "<li><a href=\"/account/dashboard\"><i class=\"fa fa-th-large\"></i><span class=\"nav-label\">Dashboard</span></a></li>";
            string employeesLink = "<li><a href=\"/account/employees\"><i class=\"fa fa-group\">"
                                    + "</i><span class=\"nav-label\">Employees</span><span onclick=\"location.href='/account/employees'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{employees}</ul></li>";
            string accountLink = "<li><a href=\"/account/accounts\" aria-expanded=\"true\"><i class=\"fa fa-credit-card\">"
                                    + "</i><span class=\"nav-label\">Accounts</span><span onclick=\"location.href='/account/accounts'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{accounts}</ul></li>";
            string servicesLink = "<li><a href=\"/account/services\" aria-expanded=\"true\"><i class=\"fa fa-cogs\">"
                                    + "</i><span class=\"nav-label\">Services</span><span onclick=\"location.href='/account/services'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{services}</ul></li>";

            switch(currentlySelected)
            {
                case "/account/dashboard":
                    dashboardLink = "<li class=\"active\"><a href=\"/account/dashboard\"><i class=\"fa fa-th-large\"></i><span class=\"nav-label\">Dashboard</span></a></li>";
                    break;
                case "/account/employees":
                    employeesLink = "<li class=\"active\"><a href=\"#\"><i class=\"fa fa-group\">"
                                    + "</i><span class=\"nav-label\">Employees</span><span onclick=\"location.href='/account/employees'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{employees}</ul></li>";
                    break;
                case "/account/employee":
                    employeesLink = "<li class=\"active\"><a href=\"#\"><i class=\"fa fa-group\">"
                                    + "</i><span class=\"nav-label\">Employees</span><span onclick=\"location.href='/account/employees'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{employees}</ul></li>";
                    break;
                case "/account/accounts":
                    accountLink = "<li class=\"active\" aria-expanded=\"true\"><a href=\"/account/accounts\"><i class=\"fa fa-credit-card\">"
                                    + "</i><span class=\"nav-label\">Accounts</span><span onclick=\"location.href='/account/accounts'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{accounts}</ul></li>";
                    break;
                case "/account/services":
                    servicesLink = "<li class=\"active\" aria-expanded=\"true\"><a href=\"/account/services\"><i class=\"fa fa-cogs\">"
                                    + "</i><span class=\"nav-label\">Services</span><span onclick=\"location.href='/account/services'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{services}</ul></li>";
                    break;
                case "/account/service":
                    servicesLink = "<li class=\"active\" aria-expanded=\"true\"><a href=\"/account/services\"><i class=\"fa fa-cogs\">"
                                    + "</i><span class=\"nav-label\">Services</span><span onclick=\"location.href='/account/services'\" class=\"label label-warning pull-right\">Manage</span><span class=\"fa arrow\" style='margin-right:10px'></span></a>" +
                                    "<ul class=\"nav nav-second-level\">" +
                                        "{services}</ul></li>";
                    break;
            }
            currentlySelected = Request.Url.PathAndQuery.ToLower();
            string returnHtml = dashboardLink + accountLink + employeesLink + servicesLink; //dashboardLink + employeesLink + accountLink;
            List<Saastrack.DAL.Account> accounts = AccountOperations.GetAllAccountsForUserInInstitutionOrder(DatabaseName);
            string instName = string.Empty;
            int i = 0;
            var accountHtml = "";
            foreach (var account in accounts)
            {
                isSelected = string.Empty;
                string newLink = "/account/dashboard?account=" + account.Id;
                if(newLink.ToLower() == currentlySelected)
                {
                    isSelected = "class=\"active\"";
                }
                
                accountHtml += "<li " + isSelected + "><a href=\"" + newLink + "\">" + InstitutionOperations.GetInstitutionName(account.institution_type) 
                                    + " - " + account.meta_name  +"</a></li>";               
            }

            returnHtml = returnHtml.Replace("{accounts}", accountHtml);

            List<Saastrack.DAL.CompanyUser> employees = CompanyUserOperations.GetAllUsers(DatabaseName);            
            var employeeHtml = "";
            foreach (var emp in employees)
            {
                isSelected = string.Empty;
                string newLink = "/account/employee?id=" + emp.id;
                if (newLink.ToLower() == currentlySelected)
                {
                    isSelected = "class=\"active\"";
                }

                employeeHtml += "<li " + isSelected + "><a href=\"" + newLink + "\">" + emp.firstName + " " + emp.lastName +"</a></li>";
            }

            returnHtml = returnHtml.Replace("{employees}", employeeHtml);

            List<Saastrack.DAL.Service> services = ServiceOperations.GetServicesForUser(DatabaseName);
            var serviceHtml = "";
            foreach (var service in services)
            {
                isSelected = string.Empty;
                string newLink = "/account/service?id=" + service.Id;
                if (newLink.ToLower() == currentlySelected)
                {
                    isSelected = "class=\"active\"";
                }

                serviceHtml += "<li " + isSelected + "><a href=\"" + newLink + "\">" + service.MasterServiceName+
                                     "</a></li>";
            }

            returnHtml = returnHtml.Replace("{services}", serviceHtml);

            return returnHtml;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                accountHtml.Text = GetMenuHTML();
                CompanyUser user = CompanyUserOperations.GetUser(HttpContext.Current.User.Identity.GetUserId(), HttpContext.Current.User.Identity.Name.ToString());
                currentUserName.Text = user.firstName + " " + user.lastName;
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