using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Saastrack.DAL.Operations
{
    public class ServiceOperations: BaseOperations
    {   
        public static List<Service> GetAllMasterServices()
        {
            List<Service> returnDbName = new List<Service>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Services", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Service newService = new Service();
                        newService.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                        newService.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                        newService.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                        newService.MasterServiceUrl = reader["Url"].ToString();
                        newService.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());
                        newService.MasterServiceName = reader["Name"].ToString();
                        returnDbName.Add(newService);
                    }
                }
            }
            return returnDbName;
        }

        public static Service InsertService(string databaseName, Service service)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                if (!db.Service.Any(e => e.MasterServiceId == service.MasterServiceId))
                {
                    service = db.Service.Add(service);
                    db.SaveChanges();
                }
            }

            return service;
        }

        public static void UpdateService(string databaseName, Service service)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var plan = db.Service.Where(i => i.Id == service.Id);
                if(plan != null)
                {
                    var serviceSelect = plan.First();
                    serviceSelect.MasterServiceId = service.MasterServiceId;
                    db.SaveChanges();
                }
                
            }
        }

        public static void UpdateServiceUrlsAndService(string databaseName, Service service)
        {
            using (SqlConnection con = new SqlConnection(connectionStringWithUser(databaseName)))
            {
                con.Open();
                string sql = "DELETE FROM ServiceUrl Where Service_Id = " + service.Id + ";";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteScalar();
                }

                foreach(ServiceUrl servUrl in service.serviceurls)
                {
                    sql = "INSERT INTO ServiceUrl (Url, Service_Id) " + "VALUES ('" + servUrl.Url + "', " + service.Id + ");";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteScalar();
                    }
                }                
            }

            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var plan = db.Service.Where(i => i.Id == service.Id);
                if (plan != null)
                {
                    var serviceSelect = plan.First();
                    serviceSelect.MasterServiceId = service.MasterServiceId;
                    serviceSelect.InactiveWhenDays = service.InactiveWhenDays;
                    db.SaveChanges();
                }

            }
        }

        public static Service GetService(string databaseName, int id)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();
                    Service temp = db.Service.Where(m => m.Id == id).Include(m => m.serviceusage).Include(m => m.serviceurls).First();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Services where Id=" + temp.MasterServiceId, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Service newService = new Service();
                            newService.Id = id;
                            newService.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                            newService.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                            newService.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                            newService.MasterServiceUrl = reader["Url"].ToString();
                            newService.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());
                            newService.MasterServiceUri = new Uri(reader["Url"].ToString());
                            newService.MasterServiceName = reader["Name"].ToString();
                            newService.serviceurls = temp.serviceurls;
                            newService.AddedByBankFeed = temp.AddedByBankFeed;
                            newService.InactiveWhenDays = temp.InactiveWhenDays;
                            return newService;
                        }
                    }
                }
            }
            return null;
        }

        public static List<Service> GetServicesForUser(string databaseName)
        {
            List<Service> returnDbName = new List<Service>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var services = db.Service.Include(m => m.serviceusage).Include(m => m.serviceurls).ToList();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();
                    foreach (Service serv in services)
                    {                        
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Services where Id=" + serv.MasterServiceId, con))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Service newService = new Service();
                                newService.Id = serv.Id;
                                newService.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                                newService.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                                newService.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                                newService.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());
                                newService.MasterServiceUrl = reader["Url"].ToString();
                                newService.MasterServiceUri = new Uri(reader["Url"].ToString());
                                newService.MasterServiceName = reader["Name"].ToString();
                                newService.serviceurls = serv.serviceurls;
                                returnDbName.Add(newService);
                            }
                        }

                    }
                }
               
            }
            
            return returnDbName;
        }

        public static void AddDetectedServiceToCompanyUser(string databaseName, int companyUserId, int serviceId, bool preaddedService)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var plan = db.CompanyUserService.Where(i => i.companyuserid == companyUserId && i.serviceid == serviceId);
                if (plan == null || !plan.Any())
                {
                    CompanyUserServices newService = new CompanyUserServices();
                    newService.companyuserid = companyUserId;
                    newService.AddedManually = preaddedService;
                    newService.DateAdded = DateTime.Now;
                    newService.DetectedAutomatically = !preaddedService;
                    newService.LastLogin = DateTime.Now;
                    newService.serviceid = serviceId;

                    db.CompanyUserService.Add(newService);
                }                
            }
        }

        public static List<Service> GetServicesForCompanyUser(string databaseName, int companyUserId)
        {
            List<Service> returnDbName = new List<Service>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var services = db.Service.Include(m => m.serviceusage).ToList();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();
                    foreach (Service serv in services)
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Services where Id=" + serv.MasterServiceId, con))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Service newService = new Service();
                                newService.Id = serv.Id;
                                newService.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                                newService.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                                newService.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());
                                newService.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                                newService.MasterServiceUrl = reader["Url"].ToString();
                                newService.MasterServiceUri = new Uri(reader["Url"].ToString());
                                newService.MasterServiceName = reader["Name"].ToString();
                                returnDbName.Add(newService);
                            }
                        }

                    }
                }

            }

            return returnDbName;
        }

        public static TimeSpan GetTotalTimeSpentForService(string databaseName, int serviceId)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                double totalserviceUsageInSeconds = db.ServiceUsage.Where(d => d.serviceid == serviceId).Sum(d => d.Seconds);
                return TimeSpan.FromSeconds(totalserviceUsageInSeconds);
            }
        }

        public static List<CompanyUserServices> GetCompanyUserServicesForCompany(string databaseName)
        {
            List<CompanyUserServices> returnDbName = new List<CompanyUserServices>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var services = db.CompanyUserService.Include(c => c.service).Include(c => c.service.serviceusage).Include(c => c.companyuser).ToList();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();
                    foreach (CompanyUserServices serv in services)
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Services where Id=" + serv.service.MasterServiceId, con))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                serv.service.Id = serv.Id;
                                serv.service.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                                serv.service.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());
                                serv.service.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                                serv.service.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                                serv.service.MasterServiceUrl = reader["Url"].ToString();
                                serv.service.MasterServiceUri = new Uri(reader["Url"].ToString());
                                serv.service.MasterServiceName = reader["Name"].ToString();

                                var servicesUsage = serv.service.serviceusage.Where(s => s.UsageDate >= DateTime.Now.AddDays(-30) && s.UsageDate <= DateTime.Now).Sum(d => d.Seconds);
                                if (servicesUsage > 0)
                                {
                                    serv.TotalMinutesSpent = Convert.ToInt32((double)servicesUsage / 60.0);
                                }
                                else
                                {
                                    serv.TotalMinutesSpent = 0;
                                }
                                //get last login
                                var lastLogin = serv.service.serviceusage.OrderByDescending(d => d.UsageDate);
                                if (lastLogin != null)
                                {
                                    serv.LastLogin = lastLogin.First().UsageDate;
                                }

                                returnDbName.Add(serv);
                            }
                        }

                    }
                }

            }

            return returnDbName;
        }

        public static List<CompanyUserServices> GetCompanyUserServicesForCompanyUser(string databaseName, int companyUserId)
        {
            List<CompanyUserServices> returnDbName = new List<CompanyUserServices>();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                var services = db.CompanyUserService.Include(c => c.service).Include(c => c.service.serviceusage).Include(c => c.companyuser).Where(d => d.companyuserid == companyUserId).ToList();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    con.Open();
                    foreach (CompanyUserServices serv in services)
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Services where Id=" + serv.service.MasterServiceId, con))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                serv.service.Id = serv.Id;
                                serv.service.MasterServiceId = Convert.ToInt32(reader["Id"].ToString());
                                serv.service.MasterServiceImageUrl = reader["ImageUrl"].ToString();
                                serv.service.MasterServiceIsApp = Convert.ToBoolean(reader["IsApp"].ToString());
                                serv.service.MasterServiceUrl = reader["Url"].ToString();
                                serv.service.MasterServiceUri = new Uri(reader["Url"].ToString());
                                serv.service.MasterServiceName = reader["Name"].ToString();
                                serv.service.PerUser = Convert.ToBoolean(reader["PerUser"].ToString());

                                var servicesUsage = serv.service.serviceusage.Where(s=> s.UsageDate >= DateTime.Now.AddDays(-30) && s.UsageDate <= DateTime.Now).Sum(d => d.Seconds);
                                if (servicesUsage > 0)
                                {
                                    serv.TotalMinutesSpent = Convert.ToInt32((double)servicesUsage / 60.0);
                                }
                                else
                                {
                                    serv.TotalMinutesSpent = 0;
                                }
                                //get last login
                                var lastLogin = serv.service.serviceusage.OrderByDescending(d => d.UsageDate);
                                if (lastLogin != null)
                                {
                                    serv.LastLogin = lastLogin.First().UsageDate;
                                }

                                returnDbName.Add(serv);
                            }
                        }

                    }
                }

            }

            return returnDbName;
        }
    }
}
