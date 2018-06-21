using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoringSystem.Models;

namespace ScoringSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ScoringSystem.Models.Division> Division { get; set; }

        public DbSet<ScoringSystem.Models.Grade> Grade { get; set; }

        public DbSet<ScoringSystem.Models.PowerFactor> PowerFactor { get; set; }

        public DbSet<ScoringSystem.Models.User> User { get; set; }

        public DbSet<ScoringSystem.Models.Score> Score { get; set; }
    }
}
