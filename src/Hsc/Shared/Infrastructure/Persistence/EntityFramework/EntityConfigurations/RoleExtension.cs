using Hsc.Roles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class RoleExtension
    {
        public static ModelBuilder BuildRole(this ModelBuilder modelBuilder)
        {
            var roleEntity = modelBuilder.Entity<Role>();

            roleEntity.ToTable(Role.TableName);
            roleEntity.HasKey(role => role.Id);

            roleEntity
                .Property(role => role.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasAnnotation("RequiredErrorMessage", "Id is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            roleEntity
                .Property(role => role.Description)
                .IsRequired()
                .HasColumnName("Descripcion")
                .HasAnnotation("RequiredErrorMessage", "Descripcion is required")
                .HasMaxLength(50);

            roleEntity
                .HasMany(role => role.Users)
                .WithOne(userRoles => userRoles.Role)
                .HasForeignKey(userRoles => userRoles.RoleId);

            roleEntity
                .HasMany(role => role.Functionalities)
                .WithOne(roleFunctionalities => roleFunctionalities.Role)
                .HasForeignKey(roleFunctionalities => roleFunctionalities.RoleId);

            return modelBuilder;
        }
    }
}