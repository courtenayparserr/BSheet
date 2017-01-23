using Saastrack.DAL;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaasTrack.Account
{
    public partial class EmployeeSettings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    var services = ServiceOperations.GetCompanyUserServicesForCompanyUser(DatabaseName, id);
                    var allServices = ServiceOperations.GetAllMasterServices();
                    foreach(CompanyUserServices serv in services)
                    {
                        RadListBoxItem item = new RadListBoxItem(serv.service.MasterServiceName, serv.service.MasterServiceId.ToString());
                        if(serv.TotalMinutesSpent > 0)
                        {
                            item.Enabled = false;
                        }
                        servicesUsed.Items.Add(item);
                        allServices.Remove(allServices.Find(i => i.MasterServiceId == serv.service.MasterServiceId));
                    }                    

                    servicesList.DataKeyField = "MasterServiceId";
                    servicesList.DataTextField = "MasterServiceName";
                    servicesList.DataSource = allServices;
                    servicesList.DataBind();
                }
            }
        }

        protected void saveSettings_Click(object sender, EventArgs e)
        {

        }
    }
}