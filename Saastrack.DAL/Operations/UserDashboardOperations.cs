using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class UserDashboardOperations : BaseOperations
    {
        public static void UpdateUserDashboard(string databaseName, UserDashboard dash)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.UserDashboard.ToList();
                if (item != null && item.Count() > 0)
                {
                    var theAcc = item.First();
                    theAcc.Annual = dash.Annual;
                    theAcc.AnnualIncreaseDecrease = dash.AnnualIncreaseDecrease;
                    theAcc.Monthly = dash.Monthly;
                    theAcc.MonthlyIncreaseDecrease = dash.MonthlyIncreaseDecrease;
                    theAcc.Subs = dash.Subs;
                    db.SaveChanges();

                }
                else
                {
                    //add it                    
                    db.UserDashboard.Add(dash);
                    db.SaveChanges();
                }
            }
        }

        public static UserDashboard GetUserDashboard(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                if (db.UserDashboard.Count() > 0)
                {
                    return db.UserDashboard.First();
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
