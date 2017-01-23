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
    [Table("ServiceUsage")]
    public class ServiceUsage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? serviceid { get; set; }
        [ForeignKey("serviceid")]
        public Service service { get; set; }

        public int? companyuserid { get; set; }
        [ForeignKey("companyuserid")]
        public CompanyUser companyuser { get; set; }

        public DateTime UsageDate { get; set; }

        public double Seconds { get; set; }
 
    }
}
