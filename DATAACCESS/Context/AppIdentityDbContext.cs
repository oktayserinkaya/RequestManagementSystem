using CORE.IdentityEntities;
using DATAACCESS.SeedData.IdentitySeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DATAACCESS.Context
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity tabloları için seed data
            builder.ApplyConfiguration(new AppRoleSeedData());
            builder.ApplyConfiguration(new AppUserSeedData());
            builder.ApplyConfiguration(new UserRoleSeedData());
        }
    }
}