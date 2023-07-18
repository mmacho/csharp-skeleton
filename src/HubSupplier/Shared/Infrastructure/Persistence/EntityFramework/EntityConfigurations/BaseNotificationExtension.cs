using Aseme.HubSupplier.Shared.Domain.Notification;
using Microsoft.EntityFrameworkCore;

namespace aseme_api.Infrastructure.Models.HubSuppliers
{
    public static partial class BaseNotificationExtension
    {
        public static ModelBuilder BuildBaseNotification(this ModelBuilder modelBuilder)
        {
            var baseNotificationEntity = modelBuilder.Entity<BaseNotification>();

            baseNotificationEntity.ToTable(BaseNotification.TableName);

            baseNotificationEntity.HasIndex(baseNotification => baseNotification.SentState);
            baseNotificationEntity.HasIndex(baseNotification => baseNotification.EntityType);
            baseNotificationEntity.HasIndex(baseNotification => baseNotification.EntityId);

            baseNotificationEntity
                .Property(baseNotification => baseNotification.Id)
                .ValueGeneratedOnAdd();

            baseNotificationEntity
                .Property(baseNotification => baseNotification.Version)
                .IsRowVersion();

            return modelBuilder;
        }
    }
}