using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace Saastrack.DAL
{
    [Table("CompanyUser")]
    public class CompanyUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int subscription_id { get; set; }
        public string type { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool? hasSeenIntro { get; set; }
        public bool? hasSeenIntroEmployee { get; set; }

        public virtual ICollection<Service> services { get; set; }
        public virtual ICollection<ServiceUsage> serviceusage { get; set; }
    }
}
