using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SaasTrack.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Hosting;
using Saastrack.DAL.Helper;
using Saastrack.DAL.Operations;
using Saastrack.DAL;

namespace SaasTrack.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["registerComplete"] != null)
                {
                    success.Visible = true;
                }
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                //signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionWithoutDB"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        string.Format("SELECT * FROM sys.databases WHERE [name]='{0}'", user.Id),
                        conn);

                    cmd.CommandTimeout = int.MaxValue;

                    if (cmd.ExecuteScalar() == null)
                    {
                        SqlCommand cmd2 = new SqlCommand(string.Format("CREATE DATABASE [{0}];", user.Id), conn);
                        cmd2.CommandTimeout = int.MaxValue;

                        cmd2.ExecuteNonQuery();

                    }

                }

                var connString = ConfigurationManager.ConnectionStrings["DefaultConnectionUserDb"].ToString();
                connString = connString.Replace("{dbname}", user.Id);

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var fileContents = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/CreateTables.sql"));
                    fileContents = fileContents.Replace("dbname", user.Id);
                    SqlCommand cmd2 = new SqlCommand(fileContents, conn);
                    cmd2.CommandTimeout = int.MaxValue;

                    cmd2.ExecuteNonQuery();
                }

                //put it in the userdetails
                var fileContentsUserDetails = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/AddUserToUserDetails.sql"));
                SqlConnection userdetailsDB = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                fileContentsUserDetails = fileContentsUserDetails.Replace("%userid%", user.Id);
                fileContentsUserDetails = fileContentsUserDetails.Replace("%dbName%", user.Id);
                SqlCommand cmdUserDatabase = new SqlCommand(fileContentsUserDetails, userdetailsDB);
                userdetailsDB.Open();

                cmdUserDatabase.ExecuteNonQuery();
                userdetailsDB.Close();

                Company company = new Company();
                company.Name = "Company name";

                CompanyUser cu = new CompanyUser();
                cu.phone = phoneNumber.Text;
                cu.firstName = string.Empty;
                cu.lastName = string.Empty;
                cu.email = Email.Text;
                cu.type = "User";
                cu.created_at = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss") + ".000Z";
                cu.updated_at = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss") + ".000Z";

                string planName = "Free";
                CompanyUserOperations.InsertInitialCompanyUser(user.Id, company, cu);
                if (Request.QueryString["Plan"] != null)
                {
                    int planId = Convert.ToInt32(Request.QueryString["Plan"]);
                    switch (planId)
                    {
                        case 1:
                            planName = "Free";
                            break;
                        case 2:
                            planName = "Growth";
                            break;
                        case 3:
                            planName = "Business";
                            break;
                        case 4:
                            planName = "Enterprise";
                            break;
                        case 5:
                            planName = "Corporate";
                            break;
                    }
                    PlanOperations.InsertInitialPlanAndCustomerIntoStripe(user.Id, planId, Email.Text);
                }
                else
                {
                    PlanOperations.InsertInitialPlanAndCustomerIntoStripe(user.Id, 1, Email.Text);
                }


                string CustomerIoSiteId = ConfigurationManager.AppSettings["CustomerIoSiteId"].ToString();
                string CustomerIoAPIKey = ConfigurationManager.AppSettings["CustomerIoAPIKey"].ToString();

                var customerIO = new CustomerIo(CustomerIoSiteId, CustomerIoAPIKey);
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, this.Request);
                customerIO.AddUser(user.Id, callbackUrl, 1, Email.Text, true);
                customerIO.AddTrackAction(1, user.Id, "Registered_User", new LoginObject());
                customerIO.UpdateUserPlan(user.Id, 1, Email.Text, planName);
                IdentityHelper.RedirectToReturnUrl("/account/Register?registerComplete=true", Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
                ErrorMessage.Visible = true;
            }
        }
    }

    public class LoginObject
    {
    }
}