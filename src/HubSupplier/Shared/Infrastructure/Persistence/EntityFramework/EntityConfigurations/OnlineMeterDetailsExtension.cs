using Aseme.HubSupplier.OnlineMeters.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class OnlineMeterDetailsExtension
    {
        public static ModelBuilder BuildOnlineMeterDetails(this ModelBuilder modelBuilder)
        {
            var onlineMeterDetailsEntity = modelBuilder.Entity<OnlineMeterDetails>();

            onlineMeterDetailsEntity.ToTable(OnlineMeterDetails.TableName);
            onlineMeterDetailsEntity.HasKey(onlineMeterDetails => onlineMeterDetails.OnlineMeterId);

            onlineMeterDetailsEntity.HasIndex(onlineMeterDetailsEntity => onlineMeterDetailsEntity.SerialNumber);
            onlineMeterDetailsEntity.HasIndex(onlineMeterDetailsEntity => onlineMeterDetailsEntity.Manufacturer);
            onlineMeterDetailsEntity.HasIndex(onlineMeterDetailsEntity => onlineMeterDetailsEntity.ReadingDate);

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.SerialNumber)
                .IsRequired()
                .HasColumnName("SerialNumber");

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.Manufacturer)
                .IsRequired()
                .HasColumnName("Manufacturer");

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.Model)
                .IsRequired()
                .HasColumnName("Model");

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.InstallationYear)
                .IsRequired()
                .HasColumnName("InstallationYear");

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.ReadingDate)
                .IsRequired()
                .HasColumnName("ReadingDate");

            onlineMeterDetailsEntity
                .Property(onlineMeterDetails => onlineMeterDetails.Period)
                .IsRequired()
                .HasColumnName("Period");

            onlineMeterDetailsEntity
                .HasOne(onlineMeterDetails => onlineMeterDetails.OnlineMeter)
                .WithOne(onlineMeter => onlineMeter.OnlineMeterDetails)
                .HasForeignKey<OnlineMeterDetails>(onlineMeterDetails => onlineMeterDetails.OnlineMeterId);

            return modelBuilder;
        }
    }
}