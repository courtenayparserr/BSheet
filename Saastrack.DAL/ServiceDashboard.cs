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
    [Table("ServiceDashboard")]
    public class ServiceDashboard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? serviceid { get; set; }
        [ForeignKey("serviceid")]
        public Service service { get; set; }

        public int? TotalNumberOfUsers { get; set; }
        public int? TotalMinutesSpent { get; set; }
        public float? TotalSpend { get; set; }
        public float? EmployeeRating { get; set; }

        public int? ActiveUsers { get; set; } //used to work out total spend
       
    }
}
