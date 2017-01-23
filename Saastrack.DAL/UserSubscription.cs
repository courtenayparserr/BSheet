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
    [Table("UserSubscription")]
    public class UserSubscription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? Period { get; set; } 
        public DateTime? NextBillingDate { get; set; }
        public DateTime? PreviousBillingDate { get; set; }
        public DateTime? DateCreated { get; set; }

        [NotMapped]
        public string MotherSubscriptionName { get; set; }
        [NotMapped]
        public double LastPaymentAmount { get; set; }
        [NotMapped]
        public string MotherSubscriptionLogoUrl { get; set; }

        public int motherSubscriptionId { get; set; }        
        public virtual ICollection<UserSubscriptionPayment> payments { get; set; }

        public int? accountid { get; set; }
        [ForeignKey("accountid")]
        public Account account { get; set; }
    }
}
