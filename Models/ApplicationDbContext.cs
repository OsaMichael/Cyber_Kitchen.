using Cyber_Kitchen.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<SummaryReport> SummaryReports { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public ApplicationDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}