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
    [Table("UserDashboard")]
    public class UserDashboard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Monthly { get; set; }
        public double MonthlyIncreaseDecrease { get; set; }
        public double Annual { get; set; }
        public double AnnualIncreaseDecrease { get; set; }
        public double? PotentialSavings { get; set; }
        public int? InactiveSeats { get; set; }
        public string EmployeeEngagement { get; set; } 
        public int Subs { get; set; } 
    }
}
