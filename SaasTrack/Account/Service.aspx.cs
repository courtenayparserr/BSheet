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
    public partial class ServiceApp : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    Service service = ServiceOperations.GetService(DatabaseName, id);
                    ServiceDashboard dash = ServiceDashboardOperations.GetServiceDashboard(DatabaseName, id);
                    serviceMonthlyMinutes.Text = dash.TotalMinutesSpent.ToString();
                    serviceName.Text = service.MasterServiceName;
                    serviceName1.Text = service.MasterServiceName;
                    Literal1.Text = service.MasterServiceName;
                    Literal2.Text = service.MasterServiceName;
                    Literal3.Text = service.MasterServiceName;
                    Literal4.Text = service.MasterServiceName;
                    serviceSubscriptions.Text = dash.TotalNumberOfUsers.ToString();
                    serviceSubscriptionSpend.Text = String.Format("{0:C}", dash.TotalSpend).TrimEnd(')').TrimStart('(');
                    serviceRatingOfTen.Text = dash.EmployeeRating.ToString();
                    appImage.Src = service.MasterServiceImageUrl;

                    employeeRepeater.DataSource = CompanyUserOperations.GetUsersForService(DatabaseName, id, DateTime.Now.AddDays(-30), DateTime.Now);
                    employeeRepeater.DataBind();
                }
            }
        }

    }
}