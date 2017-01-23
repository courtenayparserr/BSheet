using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Saastrack.DAL
{
    [Table("Plan")]
    public class Plan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public bool private_topics { get; set; }
        public int sites { get; set; }
        public int topics_per_sites { get; set; }
        public bool embed_player { get; set; }
        public DateTime datecreated { get; set; }

    }
}
