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
    [Table("Account")]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string _id { get; set; }
        public string _item { get; set; }
        public string balance_available { get; set; }
        public string balance_current { get; set; }
        public string institution_type { get; set; }
        public string meta_name { get; set; }
        public string meta_number { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
        public DateTime? lastdateprocessed { get; set; }
        public DateTime? lastdateadded { get; set; }
        public bool enabled { get; set; }

        public double? Monthly { get; set; }
        public double? MonthlyIncreaseDecrease { get; set; }
        public double? Annual { get; set; }
        public double? AnnualIncreaseDecrease { get; set; }
        public int? Subs { get; set; } 

    }
}
