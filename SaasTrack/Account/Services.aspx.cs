using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Saastrack.DAL.Operations;
using Saastrack.DAL;

namespace SaasTrack.Account
{
    public partial class Services : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!CookieExists())
                {
                    var hasSeen = CompanyUserOperations.HasSeenIntros(DatabaseName, User.Identity.Name);
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
            if(HttpContext.Current.Request.Cookies["showServices"] != null)
            {
                return true;
            }

            return false;
        }

        public string GetLocalUser()
        {
            return DatabaseName;
        }

        public string GetLocalUserEmail()
        {
            return User.Identity.Name;
        }

        protected void gridServices_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
            RadComboBox combo = (RadComboBox)editedItem.FindControl("RadCombobox1");

            Service cu = new Service();
            cu.MasterServiceId = Convert.ToInt32(combo.SelectedValue);

            foreach (KeyValuePair<string, string> item in newValues)
            {
                switch (item.Key)
                {
                    case "id":
                        cu.Id = Convert.ToInt32(item.Value);
                        break;
                }
            }

            ServiceOperations.UpdateService(DatabaseName, cu);  
        }

        protected void gridServices_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
            RadComboBox combo = (RadComboBox)editedItem.FindControl("RadCombobox1");

            Service cu = new Service();
            cu.MasterServiceId = Convert.ToInt32(combo.SelectedValue);
            cu.DateAdded = DateTime.Now;
            cu.AutoDetected = false;
            cu.AddedByBankFeed = false;
            cu.InactiveWhenDays = 30; //default

            ServiceOperations.InsertService(DatabaseName, cu);

            Response.Redirect("/account/services");
        }

        protected void gridServices_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = ServiceOperations.GetServicesForUser(DatabaseName);

        }

        protected void RadCombobox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox comboBox = (RadComboBox)sender;
            foreach(Service serv in ServiceOperations.GetAllMasterServices())
            {
                if (!string.IsNullOrEmpty(e.Text))
                {
                    if(serv.MasterServiceName.ToLower().StartsWith(e.Text.ToLower()))
                    {
                        RadComboBoxItem item = new RadComboBoxItem(serv.MasterServiceName, serv.MasterServiceId.ToString());
                        item.ImageUrl = serv.MasterServiceImageUrl;
                        comboBox.Items.Add(item);
                    }                    
                }
                else
                {
                    RadComboBoxItem item = new RadComboBoxItem(serv.MasterServiceName, serv.MasterServiceId.ToString());
                    item.ImageUrl = serv.MasterServiceImageUrl;
                    comboBox.Items.Add(item);
                }
            }            
        }

        protected void gridServices_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Label lbl = (Label)item.FindControl("Label1");//accessing Label
                if(lbl.Text == "False")
                {
                    lbl.Text = "No";
                }
                else
                {
                    lbl.Text = "Yes";
                }

                Label lbl1 = (Label)item.FindControl("Label2");//accessing Label
                if (lbl1.Text == "" || lbl1.Text == "False")
                {
                    lbl1.Text = "No";
                }
                else
                {
                    lbl1.Text = "Yes";
                }
                
            }
        }

        protected void gridServices_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                e.Item.OwnerTableView.GetColumn("Settings").Display = false;                
            }
        }
    }
}