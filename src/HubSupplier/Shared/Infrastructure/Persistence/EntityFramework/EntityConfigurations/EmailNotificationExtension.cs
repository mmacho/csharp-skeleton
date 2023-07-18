using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.Notifications.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class EmailNotificationExtension
    {
        public static ModelBuilder BuildEmailNotification(this ModelBuilder modelBuilder)
        {
            var emailNotificationEntity = modelBuilder.Entity<EmailNotification>();

            emailNotificationEntity.ToTable(EmailNotification.TableName);

            emailNotificationEntity.HasIndex(emailNotification => emailNotification.EmailAddress);

            emailNotificationEntity
                .HasOne<BaseNotification>()
                .WithOne()
                .HasForeignKey<EmailNotification>(emailNotification => emailNotification.Id)
                .OnDelete(DeleteBehavior.Cascade);

            emailNotificationEntity
                .Property(emailNotification => emailNotification.EmailAddress)
                .IsRequired()
                .HasColumnName("EmailAddress")
                .HasAnnotation("RequiredErrorMessage", "EmailAddress is required")
                .HasMaxLength(320);

            return modelBuilder;
        }
    }
}