using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
  
        public partial class Voter
        {
            public int VoterId { get; set; }
            public string VotName { get; set; }
            public string StaffNo { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
        }
}