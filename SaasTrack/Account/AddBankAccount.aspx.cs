using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaasTrack.Account
{
    public partial class AddBankAccount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bankAccounts.DataSource = AccountOperations.GetAllAccountsIncludingDisabledForUser(DatabaseName);
                bankAccounts.DataBind();

                if(bankAccounts.Items.Count == 0)
                {
                    bankAccounts.Visible = false;
                    emptyRepeater.Visible = true;
                }
            }
        }

        public string GetLocalUser()
        {
            return DatabaseName;
        }

        public string GetAmount(object amount)
        {
            if(amount != null)
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
                if(amount is int)
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
    }
}