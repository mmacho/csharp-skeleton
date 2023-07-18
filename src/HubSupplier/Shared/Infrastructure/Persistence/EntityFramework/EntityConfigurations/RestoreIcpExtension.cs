using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class RestoreIcpExtension
    {
        public static ModelBuilder BuildRestoreIcp(this ModelBuilder modelBuilder)
        {
            var restoreIcpEntity = modelBuilder.Entity<RestoreIcp>();

            restoreIcpEntity.ToTable(RestoreIcp.TableName);

            restoreIcpEntity.HasIndex(restoreIcp => restoreIcp.SupplyPoint);
            restoreIcpEntity.HasIndex(restoreIcp => restoreIcp.SerialNumber);

            restoreIcpEntity
                .HasOne<BaseOperation>()
                .WithOne()
                .HasForeignKey<RestoreIcp>(restoreIcp => restoreIcp.Id)
                .OnDelete(DeleteBehavior.Cascade);

            restoreIcpEntity
                .Property(restoreIcp => restoreIcp.SupplyPoint)
                .IsRequired()
                .HasColumnName("SupplyPoint")
                .HasAnnotation("RequiredErrorMessage", "SupplyPoint is required")
                .HasAnnotation("RegularExpression", "[A-Z0-9]{1,22}")
                .HasMaxLength(22);

            restoreIcpEntity
                .Property(restoreIcp => restoreIcp.SerialNumber)
                .HasColumnName("SerialNumber")
                .HasMaxLength(15);

            restoreIcpEntity
                .Property(restoreIcp => restoreIcp.OperationStatus)
                .IsRequired()
                .HasColumnName("OperationStatus")
                .HasAnnotation("RequiredErrorMessage", "OperationStatus is required");

            restoreIcpEntity
                .HasOne(restoreIcp => restoreIcp.RestoreIcpDetails)
                .WithOne()
                .HasForeignKey<RestoreIcpDetails>(restoreIcpDetails => restoreIcpDetails.RestoreIcpId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
    }
}