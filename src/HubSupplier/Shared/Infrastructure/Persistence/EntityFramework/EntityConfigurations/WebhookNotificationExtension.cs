using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.HubSupplier.WebhookNotifications.Domain;
using Microsoft.EntityFrameworkCore;

namespace aseme_api.Infrastructure.Models.HubSuppliers
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