using Hsc.UsersRoles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class UserRolesExtension
    {
        public static ModelBuilder BuildUserRoles(this ModelBuilder modelBuilder)
        {
            var userRolesEntity = modelBuilder.Entity<UserRoles>();

            userRolesEntity.ToTable(UserRoles.TableName);

            userRolesEntity
                .HasKey(userRoles => new { userRoles.LoginUserId, userRoles.RoleId });

            userRolesEntity
                .Property(userRoles => userRoles.LoginUserId)
                .IsRequired()
                .HasColumnName("IdUsuarioLogin")
                .HasAnnotation("RequiredErrorMessage", "IdUsuarioLogin is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            userRolesEntity
                .Property(userRoles => userRoles.RoleId)
                .IsRequired()
                .HasColumnName("IdRol")
                .HasAnnotation("RequiredErrorMessage", "IdRol is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            userRolesEntity
                .HasOne(userRoles => userRoles.User)
                .WithMany(user => user.Roles)
                .HasForeignKey(userRoles => userRoles.LoginUserId);

            userRolesEntity
                .HasOne(userRoles => userRoles.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(userRoles => userRoles.RoleId);

            return modelBuilder;
        }
    }
}