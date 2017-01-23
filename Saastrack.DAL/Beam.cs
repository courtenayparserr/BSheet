using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL
{
    public partial class Beam : DbContext
    {
        public Beam()
            : base("name=DefaultConnectionDummy")
        {
            Database.SetInitializer<Beam>(null);
        }

        public Beam(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<Beam>(null);
        }

        public virtual DbSet<CompanyUser> CompanyUsers { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<UserSubscription> UserSubscription { get; set; }
        public virtual DbSet<UserSubscriptionPayment> UserSubscriptionPayment { get; set; }
        public virtual DbSet<UserDashboard> UserDashboard { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceUsage> ServiceUsage { get; set; }
        public virtual DbSet<ServiceDashboard> ServiceDashboard { get; set; }
        public virtual DbSet<CompanyUserServices> CompanyUserService { get; set; }
        public virtual DbSet<ServiceUrl> ServiceUrl { get; set; }
    }
}
