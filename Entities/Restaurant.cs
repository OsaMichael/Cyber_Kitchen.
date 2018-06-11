using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public partial class Restaurant
    {
        [Key]
        public int RestId { get; set; }
        public string RestName { get; set; }
        public bool IsCanceled { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
        public virtual ICollection<Score> Scores { get; set; }

        public Restaurant()
        {
            this.Voters = new HashSet<Voter>();
            this.Scores = new HashSet<Score>();
        }
    }
}