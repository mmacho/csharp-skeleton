using Aseme.HubSupplier.RestoreIcps.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class RestoreIcpDetailsExtension
    {
        public static ModelBuilder BuildRestoreIcpDetails(this ModelBuilder modelBuilder)
        {
            var restoreIcpDetailsEntity = modelBuilder.Entity<RestoreIcpDetails>();

            restoreIcpDetailsEntity.ToTable(RestoreIcpDetails.TableName);
            restoreIcpDetailsEntity.HasKey(restoreIcpDetails => restoreIcpDetails.RestoreIcpId);

            restoreIcpDetailsEntity.HasIndex(restoreIcpDetails => restoreIcpDetails.RestoreIcpStatus);
            restoreIcpDetailsEntity.HasIndex(restoreIcpDetails => restoreIcpDetails.ExecutionDate);

            restoreIcpDetailsEntity
                .Property(restoreIcpDetails => restoreIcpDetails.RestoreIcpId)
                .IsRequired()
                .HasColumnName("RestoreIcpId")
                .HasAnnotation("RequiredErrorMessage", "RestoreIcpId is required");

            restoreIcpDetailsEntity
                .Property(restoreIcpDetails => restoreIcpDetails.RestoreIcpStatus)
                .IsRequired()
                .HasColumnName("RestoreIcpStatus")
                .HasAnnotation("RequiredErrorMessage", "RestoreIcpStatus is required");

            restoreIcpDetailsEntity
                .Property(restoreIcpDetails => restoreIcpDetails.ExecutionDate)
                .IsRequired()
                .HasColumnName("ExecutionDate")
                .HasAnnotation("RequiredErrorMessage", "ExecutionDate is required");

            restoreIcpDetailsEntity
                .Property(restoreIcpDetails => restoreIcpDetails.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasAnnotation("RequiredErrorMessage", "Description is required")
                .HasMaxLength(512);

            return modelBuilder;
        }
    }
}