using Aseme.HubSupplier.Shared.Domain.Operation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class ValueGeneratorExtension
    {
        public static ModelBuilder AddValueGenerator(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseOperation>(operationBaseEntity =>
            {
                operationBaseEntity.Property(operationBase => operationBase.Version).HasValueGenerator(typeof(RandomByteArrayValueGenerator));
            });

            return modelBuilder;
        }
    }

    public class RandomByteArrayValueGenerator : ValueGenerator<byte[]>
    {
        public override bool GeneratesTemporaryValues => false;

        public override byte[] Next(EntityEntry entry)
        {
            var random = new Random();
            var buffer = new byte[16];

            random.NextBytes(buffer);

            return buffer;
        }
    }
}