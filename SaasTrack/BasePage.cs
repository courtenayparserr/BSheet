using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using Saastrack.DAL;
using Saastrack.DAL.Operations;

namespace SaasTrack
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            this.AsyncMode = true;
        }

        public Plan Plan
        {
            get
            {
                if (Session["planDetails"] != null)
                {
                    if (Context.User.Identity.IsAuthenticated)
                    {
                        return (Plan)Session["planDetails"];
                    }
                }
                else
                {
                    if (Context.User.Identity.IsAuthenticated)
                    {
                        Plan plan = PlanOperations.GetPlanForCompany(DatabaseName);
                        Session["planDetails"] = plan;
                        return plan;
                    }
                }

                return null;
            }
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
                    Session["databaseName"] = UserDetailOperations.GetUserDbName(User.Identity.GetUserId());
                }
                if (string.IsNullOrEmpty(Session["databaseName"].ToString()))
                {
                    Context.GetOwinContext().Authentication.SignOut();
                    Response.Redirect("/Default.aspx");
                }
                return Session["databaseName"].ToString();
            }
        }

        public string CurrentSiteId
        {
            get
            {
                if (Session["CurrentSiteId"] != null && Session["CurrentSiteId"].ToString() != string.Empty)
                {
                    return Session["CurrentSiteId"].ToString();
                }
                else
                {
                    Response.Redirect("account/Dashboard");
                }

                return null;
            }
            set
            {
                Session["CurrentSiteId"] = value;
            }
        }

        public void ShowSuccessMessage(string title, string message, ref Panel panel,
            ref Literal heading, ref Literal description)
        {
            panel.Visible = true;
            heading.Text = title;
            description.Text = message;
            panel.CssClass = "alert alert-success";
        }

        public void ShowInfoMessage(string title, string message, ref Panel panel,
            ref Literal heading, ref Literal description)
        {
            panel.Visible = true;
            heading.Text = title;
            description.Text = message;
            panel.CssClass = "alert alert-info";
        }

        public void ShowWarningMessage(string title, string message, ref Panel panel,
            ref Literal heading, ref Literal description)
        {
            panel.Visible = true;
            heading.Text = title;
            description.Text = message;
            panel.CssClass = "alert alert-warning";
        }

        public void ShowErrorMessage(string title, string message, ref Panel panel,
            ref Literal heading, ref Literal description)
        {
            panel.Visible = true;
            heading.Text = title;
            description.Text = message;
            panel.CssClass = "alert alert-danger";
        }
    }
}