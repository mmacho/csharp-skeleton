using Aseme.HubSupplier.OnlineMeters.Domain;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Microsoft.EntityFrameworkCore;

namespace aseme_api.Infrastructure.Models.HubSuppliers
{
    public static partial class OnlineMeterExtension
    {
        public static ModelBuilder BuildOnlineMeter(this ModelBuilder modelBuilder)
        {
            var onlineMeterEntity = modelBuilder.Entity<OnlineMeter>();

            onlineMeterEntity.ToTable(OnlineMeter.TableName);

            onlineMeterEntity.HasIndex(onlineMeter => onlineMeter.SupplyPoint);
            onlineMeterEntity.HasIndex(onlineMeter => onlineMeter.OperationStatus);

            onlineMeterEntity
                .HasOne<BaseOperation>()
                .WithOne()
                .HasForeignKey<OnlineMeter>(onlineMeter => onlineMeter.Id)
                .OnDelete(DeleteBehavior.Cascade);

            onlineMeterEntity
                .Property(onlineMeter => onlineMeter.SupplyPoint)
                .HasColumnName("SupplyPoint")
                .IsRequired()
                .HasAnnotation("RequiredErrorMessage", "SupplyPoint is required")
                .HasAnnotation("RegularExpression", "[A-Z0-9]{1,22}")
                .HasMaxLength(22);

            onlineMeterEntity
                .Property(onlineMeter => onlineMeter.OperationStatus)
                .HasColumnName("OperationStatus")
                .IsRequired()
                .HasAnnotation("RequiredErrorMessage", "SupplyPoint is required");

            onlineMeterEntity
                .HasOne(onlineMeter => onlineMeter.OnlineMeterDetails)
                .WithOne()
                .HasForeignKey<OnlineMeterDetails>(onlineMeter => onlineMeter.OnlineMeterId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
    }
}