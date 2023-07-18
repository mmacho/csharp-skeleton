using Hsc.Functionalities.Domain;
using Hsc.Logins.Domain;
using Hsc.Roles.Domain;
using Hsc.RolesFunctionalities.Domain;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
using Hsc.UsersRoles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework
{
    public class HscDbContext : DbContext
    {
        public DbSet<Login> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Functionality> Functionalities { get; set; }

        public DbSet<RoleFunctionalities> RoleFunctionalities { get; set; }

        public HscDbContext(DbContextOptions<HscDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildLogin();
            modelBuilder.BuildRole();
            modelBuilder.BuildFunctionality();
            modelBuilder.BuildUserRoles();
            modelBuilder.BuildRoleFunctionalities();
        }
    }
}