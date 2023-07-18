using Hsd.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Hsd.Shared.Infrastructure.Persistence.EntityFramework
{
    public class HsdDbContext : DbContext
    {
        public HsdDbContext(DbContextOptions<HsdDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildUser();
        }
    }
}