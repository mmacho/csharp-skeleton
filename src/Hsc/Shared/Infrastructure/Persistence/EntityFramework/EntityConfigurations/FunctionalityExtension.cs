using Hsc.Functionalities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class FunctionalityExtension
    {
        public static ModelBuilder BuildFunctionality(this ModelBuilder modelBuilder)
        {
            var functionalityEntity = modelBuilder.Entity<Functionality>();

            functionalityEntity.ToTable(Functionality.TableName);

            functionalityEntity.HasKey(functionality => functionality.Id);

            functionalityEntity
                .Property(functionality => functionality.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasAnnotation("RequiredErrorMessage", "Id is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            functionalityEntity
                .Property(functionality => functionality.Description)
                .IsRequired()
                .HasColumnName("Descripcion")
                .HasAnnotation("RequiredErrorMessage", "Descripcion is required")
                .HasMaxLength(50);

            functionalityEntity
                .HasMany(functionality => functionality.Roles)
                .WithOne(roleFunctionalities => roleFunctionalities.Functionality)
                .HasForeignKey(roleFunctionalities => roleFunctionalities.FunctionalityId);

            return modelBuilder;
        }
    }
}