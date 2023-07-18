using Hsc.Logins.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class LoginExtension
    {
        public static ModelBuilder BuildLogin(this ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<Login>();

            userEntity.ToTable(Login.TableName);

            userEntity
                .HasKey(user => user.Id);

            userEntity
                .Property(user => user.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasAnnotation("RequiredErrorMessage", "Id is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            userEntity
                .Property(user => user.UserName)
                .IsRequired()
                .HasColumnName("Usuario")
                .HasAnnotation("RequiredErrorMessage", "Usuario is required")
                .HasMaxLength(15);

            userEntity
                .Property(user => user.Password)
                .IsRequired()
                .HasColumnName("Contraseña")
                .HasAnnotation("RequiredErrorMessage", "Contraseña is required");

            userEntity
                .Property(user => user.IsDeleted)
                .IsRequired()
                .HasColumnName("EstaBorrado")
                .HasAnnotation("RequiredErrorMessage", "EstaBorrado is required")
                .IsRequired();

            userEntity
                .Property(user => user.CreationDate)
                .IsRequired()
                .HasColumnName("FechaAlta")
                .HasAnnotation("RequiredErrorMessage", "FechaAlta is required");

            userEntity
                .Property(user => user.DeletedDate)
                .HasColumnName("FechaBaja")
                .HasAnnotation("RequiredErrorMessage", "FechaBaja is required");

            userEntity
                .Property(user => user.ModifiedDate)
                .HasColumnName("FechaModificacion")
                .HasAnnotation("RequiredErrorMessage", "FechaModificacion is required");

            userEntity
                .Property(user => user.AuthorizedThirdPartyFilterMode)
                .IsRequired()
                .HasColumnName("ModoFiltradoTercerosAutorizados")
                .HasAnnotation("RequiredErrorMessage", "ModoFiltradoTercerosAutorizados is required")
                .HasAnnotation("Range", new[] { 0, 9999 });

            return modelBuilder;
        }
    }
}