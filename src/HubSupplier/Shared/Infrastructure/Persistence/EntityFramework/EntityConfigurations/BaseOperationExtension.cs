using Aseme.HubSupplier.Shared.Domain.Operation;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class BaseOperationExtension
    {
        public static ModelBuilder BuildBaseOperation(this ModelBuilder modelBuilder)
        {
            var baseOperationEntity = modelBuilder.Entity<BaseOperation>();

            baseOperationEntity.ToTable(BaseOperation.TableName);
            baseOperationEntity.HasKey(baseOperation => baseOperation.Id);

            baseOperationEntity.HasIndex(baseOperation => baseOperation.CreatedDate);
            baseOperationEntity.HasIndex(baseOperation => baseOperation.LastModifiedDate);
            baseOperationEntity.HasIndex(baseOperation => baseOperation.Distributor);

            baseOperationEntity
                .Property(baseOperation => baseOperation.Id)
                .ValueGeneratedOnAdd();

            baseOperationEntity
                .Property(baseOperation => baseOperation.Version)
                .IsRowVersion();

            baseOperationEntity
                .Property(operationBase => operationBase.Distributor)
                .IsRequired()
                .HasColumnName("Distributor")
                .HasAnnotation("RequiredErrorMessage", "Distributor is required")
                .HasMaxLength(6);

            return modelBuilder;
        }
    }
}