using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public partial class SummaryReport
    {

        [Key]
        public int RestId { get; set; }
        public string RestName { get; set; }
        public DateTime EntryDate { get; set; }
        public int RestSum { get; set; }
    }
}