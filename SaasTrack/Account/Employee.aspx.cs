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
    public partial class Employee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    employeeRepeater.DataSource = ServiceOperations.GetCompanyUserServicesForCompanyUser(DatabaseName, id);
                    employeeRepeater.DataBind();

                    CompanyUser compUser = CompanyUserOperations.GetUserById(DatabaseName, id);
                    employeeName.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName0.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName1.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName2.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName3.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName4.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName5.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName6.Text = compUser.firstName + " " + compUser.lastName;
                    employeeName7.Text = compUser.firstName + " " + compUser.lastName;

                }
            }
        }
    }
}