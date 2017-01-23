using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class ServiceDashboardOperations : BaseOperations
    {
        public static ServiceDashboard GetServiceDashboard(string databaseName, int serviceId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.ServiceDashboard.Where(m => m.serviceid == serviceId).First();
            }
        }
    }
}
