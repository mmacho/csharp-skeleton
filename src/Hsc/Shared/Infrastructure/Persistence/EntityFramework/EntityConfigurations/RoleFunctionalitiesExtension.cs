using Hsc.RolesFunctionalities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class RoleFunctionalitiesExtension
    {
        public static ModelBuilder BuildRoleFunctionalities(this ModelBuilder modelBuilder)
        {
            var roleFunctionalitiesEntity = modelBuilder.Entity<RoleFunctionalities>();

            roleFunctionalitiesEntity.ToTable(RoleFunctionalities.TableName);

            roleFunctionalitiesEntity
                .HasKey(roleFunctionalities => new { roleFunctionalities.RoleId, roleFunctionalities.FunctionalityId });

            roleFunctionalitiesEntity
                .Property(roleFunctionalities => roleFunctionalities.RoleId)
                .HasColumnName("IdRol")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "RoleId is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            roleFunctionalitiesEntity
                .Property(roleFunctionalities => roleFunctionalities.FunctionalityId)
                .HasColumnName("IdFuncionalidad")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "FunctionalityId is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            roleFunctionalitiesEntity
                .Property(roleFunctionalities => roleFunctionalities.Value)
                .HasColumnName("Valor")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Value is required")
                .HasMaxLength(50);

            roleFunctionalitiesEntity
                .Property(roleFunctionalities => roleFunctionalities.IsQueryable)
                .HasColumnName("EsConsultableActivaEntrante")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "IsQueryable is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            roleFunctionalitiesEntity
                .HasOne(roleFunctionalities => roleFunctionalities.Role)
                .WithMany(role => role.Functionalities)
                .HasForeignKey(roleFunctionalities => roleFunctionalities.RoleId);

            roleFunctionalitiesEntity
                .HasOne(roleFunctionalities => roleFunctionalities.Functionality)
                .WithMany(functionality => functionality.Roles)
                .HasForeignKey(roleFunctionalities => roleFunctionalities.FunctionalityId);

            return modelBuilder;
        }
    }
}