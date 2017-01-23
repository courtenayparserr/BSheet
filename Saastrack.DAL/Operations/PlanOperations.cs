using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class PlanOperations : BaseOperations
    {
        public static void UpgradePlan(string databaseName, string planName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var plan = db.Plans.First();
                plan.name = planName;
                db.SaveChanges();
            }
        }

        public static void InsertInitialPlanAndCustomerIntoStripe(string databaseName, int planNumber, string email)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var planService = new StripePlanService();
                IEnumerable<StripePlan> response = planService.List();
                StripePlan stripePlan = new StripePlan();
                Plan newPlan = new Plan();
                switch (planNumber)
                {
                    case 1:
                        newPlan.name = "Free";
                        stripePlan = response.Where(i => i.Id == "Free").FirstOrDefault();
                        break;
                    case 2:
                        newPlan.name = "Growth";
                        stripePlan = response.Where(i => i.Id == "Growth").FirstOrDefault();
                        break;
                    case 3:
                        newPlan.name = "Business";
                        stripePlan = response.Where(i => i.Id == "Business").FirstOrDefault();
                        break;
                    case 4:
                        newPlan.name = "Enterprise";
                        stripePlan = response.Where(i => i.Id == "Enterprise").FirstOrDefault();
                        break;
                    case 5:
                        newPlan.name = "Corporate";
                        stripePlan = response.Where(i => i.Id == "Corporate").FirstOrDefault();
                        break;
                }

                newPlan.private_topics = true;
                newPlan.embed_player = true;
                newPlan.sites = 1;
                newPlan.topics_per_sites = Convert.ToInt32(stripePlan.Metadata["topics_per_sites"]);
                newPlan.datecreated = DateTime.Now;
                db.Plans.Add(newPlan);
                db.SaveChanges();

                var myCustomer = new StripeCustomerCreateOptions();

                // set these properties for customer
                myCustomer.Email = email;
                Dictionary<string, string> newMetaData = new Dictionary<string, string>();
                newMetaData.Add("DatabaseName", databaseName);

                myCustomer.Metadata = newMetaData;
                myCustomer.PlanId = stripePlan.Id;
                myCustomer.Quantity = 1;
                var customerService = new StripeCustomerService();
                StripeCustomer stripeCustomer = customerService.Create(myCustomer);

            }
        }

        public static Plan GetPlanForCompany(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                return db.Plans.FirstOrDefault();
            }
        }
    }
}
