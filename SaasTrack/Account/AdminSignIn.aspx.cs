using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SaasTrack.Models;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Saastrack.DAL.Operations;

namespace SaasTrack.Account
{
    public partial class AdminSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Dictionary<Guid, string> users = UserDetailOperations.GetAllDatabaseUsers();
                foreach(KeyValuePair<Guid, string> userDetails in users)
                {
                    DropDownList1.Items.Add(new ListItem(userDetails.Value, userDetails.Key.ToString()));
                }
            }
        }

        protected void signIn_Click(object sender, EventArgs e)
        {
            var result = SignIn(DropDownList1.SelectedItem.Text, DropDownList1.SelectedItem.Value, true);
            switch (result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl("account/Dashboard", Response);
                    break;
            }
        }

        public SignInStatus SignIn(string email, string userId, bool rememberMe)
        {
            bool IsValid = true;   //Validate user credential against your db here
 
            if (IsValid)
            {                
                var ident = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                ident.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                ident.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider&amp;quot;, &amp;quot;ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
                ident.AddClaim(new Claim(ClaimTypes.Name, email));
                ident.AddClaim(new Claim(ClaimTypes.Email, email));
 
                HttpContext.Current.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, ident);   //Sign In
 
                return SignInStatus.Success;
 
            }
 
            return SignInStatus.Failure;
        }
    }
}