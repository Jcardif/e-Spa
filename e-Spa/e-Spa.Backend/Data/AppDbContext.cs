using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Spa.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_Spa.Backend.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Salon> Salons { get; set; }
        public DbSet<SalonClient> SalonClients { get; set; }
        public DbSet<SalonManager> SalonManagers { get; set; }
        public DbSet<SalonService> SalonServices { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity => { entity.ToTable("AppUsers", "Security"); });

            builder.Entity<AppRole>(entity => { entity.ToTable("AppRoles", "Security"); });

            builder.Entity<IdentityUserClaim<int>>(entity => { entity.ToTable("UserClaims", "Security"); });

            builder.Entity<IdentityUserLogin<int>>(entity => { entity.ToTable("UserLogins", "Security"); });

            builder.Entity<IdentityRoleClaim<int>>(entity => { entity.ToTable("RoleClaims", "Security"); });

            builder.Entity<IdentityUserRole<int>>(entity => { entity.ToTable("UserRoles", "Security"); });


            builder.Entity<IdentityUserToken<int>>(entity => { entity.ToTable("UserTokens", "Security"); });
        }
    }
}
