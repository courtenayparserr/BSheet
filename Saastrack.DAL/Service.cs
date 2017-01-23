using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL
{
    /// <summary>
    /// In other DB
    /// </summary>
    [Table("Service")]
    public class Service
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MasterServiceId { get; set; }
        public bool AutoDetected { get; set; }
        public bool? AddedByBankFeed { get; set; }

        public DateTime? DateAdded { get; set; }
        public virtual ICollection<ServiceUsage> serviceusage { get; set; }

        public virtual ICollection<ServiceUrl> serviceurls { get; set; }
        public int? InactiveWhenDays { get; set; } //defined for the admin checked when is a service determined inactive? after 30 days? one week?

        [NotMapped]
        public string MasterServiceName { get; set; }
        [NotMapped]
        public string MasterServiceImageUrl { get; set; }
        [NotMapped]
        public bool MasterServiceIsApp { get; set; }
        [NotMapped]
        public string MasterServiceUrl { get; set; }
        [NotMapped]
        public bool? PerUser { get; set; }

        [NotMapped]
        public Uri MasterServiceUri { get; set; }
 
    }
}
