using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class ServiceUsageOperations : BaseOperations
    {
        public static ServiceUsage AddUpdateServiceUsage(string databaseName, ServiceUsage serviceUsage)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var item = db.ServiceUsage.Where(e => e.UsageDate == serviceUsage.UsageDate && e.serviceid == serviceUsage.serviceid);

                if (item == null || item.Count() == 0)
                {
                    serviceUsage = db.ServiceUsage.Add(serviceUsage);
                    db.SaveChanges();
                }
                else
                {
                    // it exists for that day, so update
                    var entry = item.First();
                    entry.Seconds = serviceUsage.Seconds;
                    db.SaveChanges();

                }
            }

            return serviceUsage;
        }
    }
}
