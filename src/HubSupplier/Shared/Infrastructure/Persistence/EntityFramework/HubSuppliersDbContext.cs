using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.OnlineMeters.Domain;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
using Aseme.HubSupplier.Shared.Infrastructure.Providers.Claims;
using Aseme.Shared.Domain;
using Aseme.Shared.Domain.HttpLogs.Domain;
using aseme_api.Infrastructure.Models.HubSuppliers;
using Hsc.Logins.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework
{
    public class HubSuppliersDbContext : DbContext
    {
        public Login? User { get; set; }

        private readonly string? _ownerId;
        private readonly string? _distributorId;

        public HubSuppliersDbContext(DbContextOptions<HubSuppliersDbContext> options, IClaimsProvider claimsProvider) : base(options)
        {
            _ownerId = claimsProvider.OwnerId;
            _distributorId = claimsProvider.DistributorId;

            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<HttpLog> HttpLog { get; set; }

        public DbSet<BaseOperation> BaseOperations { get; set; }

        public DbSet<BaseNotification> BaseNotifications { get; set; }

        public DbSet<EmailNotification> EmailNotifications { get; set; }

        public DbSet<OnlineMeter> OnlineMeters { get; set; }

        public DbSet<OnlineMeterDetails> OnlineMeterDetails { get; set; }

        public DbSet<RestoreIcp> RestoreIcps { get; set; }

        public DbSet<RestoreIcpDetails> RestoreIcpDetails { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>().ToList();

            foreach (var entry in entries)
            {
                var state = entry.State;
                var entity = entry.Entity;

                if (_ownerId != null)
                {
                    entity.OwnerId = _ownerId;
                }

                switch (state)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entity.LastModifiedDate = DateTime.UtcNow;
                        break;

                    default:
                        continue;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddQueryFilters(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BaseOperation>()
                .HasQueryFilter(operationBase =>
                    _distributorId == null
                        ? operationBase.OwnerId == _ownerId
                        : operationBase.Distributor == _distributorId);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Core
            modelBuilder.BuildBaseOperation();
            modelBuilder.BuildBaseNotification();

            // App
            modelBuilder.BuildHttpLog();
            modelBuilder.BuildEmailNotification();
            modelBuilder.BuildWebhookNotification();

            modelBuilder.BuildRestoreIcp();
            modelBuilder.BuildRestoreIcpDetails();
            modelBuilder.BuildOnlineMeter();
            modelBuilder.BuildOnlineMeterDetails();

            AddQueryFilters(modelBuilder);

            // Testing
            if (Database.IsInMemory())
            {
                modelBuilder.AddValueGenerator();
            }
        }
    }
}