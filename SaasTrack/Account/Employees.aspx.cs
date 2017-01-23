using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Saastrack.DAL.Operations;
using Saastrack.DAL;
using System.Configuration;
using Saastrack.DAL.Helper;

namespace SaasTrack.Account
{
    public class DownloadAppObject
    {
        public string email { get; set; }
    }

    public partial class Employees : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CookieExists())
                {
                    var hasSeen = CompanyUserOperations.HasSeenIntrosEmployee(DatabaseName, User.Identity.Name);
                    if (!hasSeen)
                    {
                        overallOverlay.Visible = true;
                        addAccountsOrServices.Visible = true;
                    }
                }
            }
        }

        private bool CookieExists()
        {
            if (HttpContext.Current.Request.Cookies["showEmployee"] != null)
            {
                return true;
            }

            return false;
        }

        public string GetLocalUserEmail()
        {
            return User.Identity.Name;
        }
        
        public string GetServicesForEmployee(object services)
        {
            return "yes";
        }

        public string GetLocalUser()
        {
            return DatabaseName;
        }

        protected void gridEmployees_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = CompanyUserOperations.GetAllUsers(DatabaseName); 
        }

        protected void gridEmployees_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            CompanyUser cu = new CompanyUser();
            foreach(KeyValuePair<string, string> item in newValues)
            {
                switch(item.Key)
                {
                    case "id":
                        cu.id = Convert.ToInt32(item.Value);
                        break;
                    case "firstName":
                        cu.firstName = item.Value;
                        break;
                    case "lastName":
                        cu.lastName = item.Value;
                        break;
                    case "email":
                        cu.email = item.Value;
                        break;
                    case "phone":
                        cu.phone = item.Value;
                        break;
                }
            }

            CompanyUserOperations.UpdateCompanyUser(DatabaseName, cu);

        }

        protected void gridEmployees_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            CompanyUser cu = new CompanyUser();
            foreach (KeyValuePair<string, string> item in newValues)
            {
                switch (item.Key)
                {                    
                    case "firstName":
                        cu.firstName = item.Value;
                        break;
                    case "lastName":
                        cu.lastName = item.Value;
                        break;
                    case "email":
                        cu.email = item.Value;
                        break;
                    case "phone":
                        cu.phone = item.Value;
                        break;
                }
            }

            var user = CompanyUserOperations.InsertInitialCompanyUser(DatabaseName, cu);
            string CustomerIoSiteId = ConfigurationManager.AppSettings["CustomerIoSiteId"].ToString();
            string CustomerIoAPIKey = ConfigurationManager.AppSettings["CustomerIoAPIKey"].ToString();
            var customerIO = new CustomerIo(CustomerIoSiteId, CustomerIoAPIKey);
            //add user
            if(customerIO.AddEmployeeUser(DatabaseName, user.id, cu.email, true))
            {
                //send email
                DownloadAppObject obj = new DownloadAppObject();
                obj.email = cu.email;

                customerIO.AddTrackAction(user.id, DatabaseName, "download_app", obj);
            }

        }

        protected void gridEmployees_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            // using DataKey
            int str2 = Convert.ToInt32(item.GetDataKeyValue("id"));
            CompanyUserOperations.RemoveById(DatabaseName, str2);
            string CustomerIoSiteId = ConfigurationManager.AppSettings["CustomerIoSiteId"].ToString();
            string CustomerIoAPIKey = ConfigurationManager.AppSettings["CustomerIoAPIKey"].ToString();
            var customerIO = new CustomerIo(CustomerIoSiteId, CustomerIoAPIKey);
            customerIO.RemoveUser(DatabaseName, str2);

            Response.Redirect("/account/employees");
        }

        protected void RadCombobox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox comboBox = (RadComboBox)sender;
            int id = Convert.ToInt32(comboBox.ToolTip);
            List<int> services = new List<int>(); 
            foreach(var item in ServiceOperations.GetCompanyUserServicesForCompanyUser(DatabaseName, id).ToList())
            {
                services.Add(item.service.MasterServiceId);
            }
            foreach (Service serv in ServiceOperations.GetAllMasterServices())
            {
                RadComboBoxItem item = new RadComboBoxItem(serv.MasterServiceName, serv.MasterServiceId.ToString());
                if(services.Contains(serv.MasterServiceId))
                {
                    item.Checked = true;
                }
                if (!string.IsNullOrEmpty(e.Text))
                {
                    if (serv.MasterServiceName.ToLower().StartsWith(e.Text.ToLower()))
                    {                        
                        item.ImageUrl = serv.MasterServiceImageUrl;                        
                        comboBox.Items.Add(item);
                    }
                }
                else
                {
                    
                    item.ImageUrl = serv.MasterServiceImageUrl;
                    comboBox.Items.Add(item);
                }
            }
            
            //populate listbox

        }

        protected void buttonAddServiceToEmployee_Click(object sender, EventArgs e)
        {

        }
    }
}