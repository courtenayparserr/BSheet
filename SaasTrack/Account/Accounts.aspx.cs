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
    public partial class Accounts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string GetLocalUser()
        {
            return DatabaseName;
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

        public string GetUpDown(object amount)
        {
            if (amount != null)
            {
                if (amount is int)
                {
                    if (Convert.ToInt32(amount) > 0)
                        return "up";
                    else
                        return "down";
                }

                if (amount is double)
                {
                    if (Convert.ToDouble(amount) > 0)
                        return "up";
                    else
                        return "down";
                }
            }

            return "up";
        }

        public string GetSubs(object amount)
        {
            if (amount != null)
            {
                return amount.ToString();
            }
            else
            {
                return "0";
            }
        }

        protected void gridAccounts_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = AccountOperations.GetAllAccountsIncludingDisabledForUser(DatabaseName);
        }
    }
}