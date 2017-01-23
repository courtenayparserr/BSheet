using Newtonsoft.Json;
using Saastrack.DAL;
using Saastrack.DAL.Helper;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaasTrack.Account
{
    public partial class Dashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //check if you have any accounts - if none then force redirect to addbankacc
                var count = AccountOperations.GetAllAccountsForUser(DatabaseName).Count;
                var hasInitial = UserDetailOperations.HasInitialProcessed(DatabaseName);

                if (count > 0)
                {
                    if (!hasInitial)
                    {
                        overallOverlay.Visible = true;
                        UserDashboard dash = DummyData.DummyDashboard();//UserDashboardOperations.GetUserDashboard(DatabaseName);                            
                        if (dash != null)
                        {
                            monthlyAmount.Text = String.Format("{0:C}", dash.Monthly).TrimEnd(')').TrimStart('(');
                            annualAmount.Text = String.Format("{0:C}", dash.Annual).TrimEnd(')').TrimStart('(');
                            monthlySubs.Text = dash.Subs.ToString();
                            monthlyIncreaseDecrease.Text = GetUpDown(dash.MonthlyIncreaseDecrease);
                            annualIncreaseDecrease.Text = GetUpDown(dash.AnnualIncreaseDecrease);
                            monthlySubsIncreaseDecrease.Text = "5";
                        }

                        subscriptions.DataSource = DummyData.DummyListOfSubscriptions();
                        subscriptions.DataBind();
                    }
                    else
                    {
                        if (Request.QueryString["account"] != null)
                        {
                            Saastrack.DAL.Account acc = AccountOperations.GetAccount(DatabaseName, Convert.ToInt32(Request.QueryString["account"].ToString()));
                            if (acc != null)
                            {
                                monthlyAmount.Text = String.Format("{0:C}", acc.Monthly).TrimEnd(')').TrimStart('(');
                                annualAmount.Text = String.Format("{0:C}", acc.Annual).TrimEnd(')').TrimStart('(');
                                monthlySubs.Text = acc.Subs.ToString();
                                if (acc.MonthlyIncreaseDecrease.HasValue)
                                    monthlyIncreaseDecrease.Text = GetUpDown(acc.MonthlyIncreaseDecrease.Value);
                                else
                                    monthlyIncreaseDecrease.Text = "up";
                                if (acc.AnnualIncreaseDecrease.HasValue)
                                    annualIncreaseDecrease.Text = GetUpDown(acc.AnnualIncreaseDecrease.Value);
                                else
                                    annualIncreaseDecrease.Text = "up";
                                //TODO
                                monthlySubsIncreaseDecrease.Text = GetUpDown(5);
                            }

                            subscriptions.DataSource = UserSubscriptionOperations.GetActiveSubscriptionsForAcc(DatabaseName, acc.Id);
                            subscriptions.DataBind();

                            //inactiveSubs.DataSource = UserSubscriptionOperations.GetInActiveSubscriptionsForAcc(DatabaseName, acc.Id);
                            //inactiveSubs.DataBind();

                            otherRecurring.DataSource = UserSubscriptionOperations.GetRecurringSubscriptionsForAcc(DatabaseName, acc.Id);
                            otherRecurring.DataBind();

                            //inactiveRecurring.DataSource = UserSubscriptionOperations.GetInactiveRecurringSubscriptionsForAcc(DatabaseName, acc.Id);
                            //inactiveRecurring.DataBind();
                        }
                        else
                        {
                            UserDashboard dash = UserDashboardOperations.GetUserDashboard(DatabaseName);//UserDashboardOperations.GetUserDashboard(DatabaseName);                            
                            if (dash != null)
                            {
                                monthlyAmount.Text = String.Format("{0:C}", dash.Monthly).TrimEnd(')').TrimStart('(');
                                annualAmount.Text = String.Format("{0:C}", dash.Annual).TrimEnd(')').TrimStart('(');
                                monthlySubs.Text = dash.Subs.ToString();
                                monthlyIncreaseDecrease.Text = GetUpDown(dash.MonthlyIncreaseDecrease);
                                annualIncreaseDecrease.Text = GetUpDown(dash.AnnualIncreaseDecrease);
                                monthlySubsIncreaseDecrease.Text = "5";
                            }
                                                        
                            subscriptions.DataSource = UserSubscriptionOperations.GetActiveSubscriptions(DatabaseName);
                            subscriptions.DataBind();

                            //inactiveSubs.DataSource = UserSubscriptionOperations.GetInActiveSubscriptions(DatabaseName);
                            //inactiveSubs.DataBind();

                            otherRecurring.DataSource = UserSubscriptionOperations.GetRecurringSubscriptions(DatabaseName);
                            otherRecurring.DataBind();

                            //inactiveRecurring.DataSource = UserSubscriptionOperations.GetInactiveRecurringSubscriptions(DatabaseName);
                            //inactiveRecurring.DataBind();
                        }
                    }
                }                
                else
                {
                    Response.Redirect("/account/addbankaccount");
                }
                
            }
        }

        public string GetShortenedName(object name)
        {
            if (name.ToString().Length > 25)
            {
                return name.ToString().Substring(0, Math.Min(name.ToString().Length, 25)) + "...";
            }
            else
            {
                return name.ToString();
            }
        }

        public string GetPeriod(object name)
        {
            if (Convert.ToInt32(name) == 1)
            {
                return "Monthly";
            }
            else
            {
                return "Annually";
            }
        }

        public string GetShortDate(object name)
        {
            if (name != null)
            {
                DateTime dt = Convert.ToDateTime(name);
                return dt.ToString("dd MMM yy");
            }
            else
            {
                return "";
            }
        }

        public string GetAmount(object amount)
        {
            if (amount != null)
            {
                return String.Format("{0:C}", Math.Abs(Convert.ToDouble(amount))).TrimEnd(')').TrimStart('(');
            }
            else
            {
                return "$0.00";
            }
        }

        public string GetLocalUser()
        {
            return DatabaseName;
        }

        public string GetUpDown(double amount)
        {
            string upDown = "up";
            if (amount < 0)
                upDown = "down";
            
            return
                "<div class=\"stat-percent font-bold text-info\">" + String.Format("{0:C}", Math.Abs(Convert.ToDouble(amount))).TrimEnd(')').TrimStart('(')  +"<i class=\"fa fa-caret-" + upDown + "\" aria-hidden=\"true\"></i>" + "</div>";
            
        }

    }
}