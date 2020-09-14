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
    /// <inheritdoc />
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        /// <inheritdoc />
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Represents Appointments Table in the Database
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Represents Reviews Table in the Database
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Represents Salons Table in the Database
        /// </summary>
        public DbSet<Salon> Salons { get; set; }
        
        /// <summary>
        /// Represents SalonServices Table in the Database
        /// </summary>
        public DbSet<SalonService> SalonServices { get; set; }

        /// <summary>
        /// Represents SalonManager Table in the Database
        /// </summary>
        public DbSet<SalonManager> SalonManagers { get; set; }


        /// <summary>
        /// Represents SalonClient Table in the Database
        /// </summary>
        public DbSet<SalonClient> SalonClients { get; set; }
        
        /// <inheritdoc />
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
