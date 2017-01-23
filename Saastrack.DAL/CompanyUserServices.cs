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
    [Table("CompanyUserServices")]
    public class CompanyUserServices
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? serviceid { get; set; }
        [ForeignKey("serviceid")]
        public Service service { get; set; }

        public int? companyuserid { get; set; }
        [ForeignKey("companyuserid")]
        public CompanyUser companyuser { get; set; }

        public DateTime DateAdded { get; set; }
        public bool AddedManually { get; set; }
        public bool DetectedAutomatically { get; set; }

        [NotMapped]
        public int TotalMinutesSpent { get; set; }
        [NotMapped]
        public DateTime LastLogin { get; set; }
    }
}
