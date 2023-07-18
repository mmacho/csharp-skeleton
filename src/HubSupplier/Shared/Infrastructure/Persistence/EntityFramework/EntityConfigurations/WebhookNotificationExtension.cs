using Aseme.HubSupplier.Notifications.Domain;
using Aseme.HubSupplier.WebhookNotifications.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class WebhookNotificationExtension
    {
        public static ModelBuilder BuildWebhookNotification(this ModelBuilder modelBuilder)
        {
            var webhookNotificationEntity = modelBuilder.Entity<WebhookNotification>();

            webhookNotificationEntity.ToTable(WebhookNotification.TableName);

            webhookNotificationEntity.HasIndex(webhookNotification => webhookNotification.Url);

            webhookNotificationEntity
                .HasOne<BaseNotification>()
                .WithOne()
                .HasForeignKey<WebhookNotification>(webhookNotification => webhookNotification.Id)
                .OnDelete(DeleteBehavior.Cascade);

            webhookNotificationEntity
                .Property(webhookNotification => webhookNotification.Url)
                .IsRequired()
                .HasColumnName("Url")
                .HasAnnotation("RequiredErrorMessage", "Url is required")
                .HasMaxLength(75);

            return modelBuilder;
        }
    }
}