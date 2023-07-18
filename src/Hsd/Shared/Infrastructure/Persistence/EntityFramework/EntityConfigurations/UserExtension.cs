using Hsd.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hsd.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static partial class UserExtension
    {
        public static ModelBuilder BuildUser(this ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<User>();

            userEntity.ToTable(User.TableName);
            userEntity.HasNoKey();

            userEntity
                .Property(user => user.Name)
                .IsRequired()
                .HasColumnName("Nombre")
                .HasAnnotation("RequiredErrorMessage", "Name is required")
                .HasMaxLength(450);

            userEntity
                .Property(user => user.Password)
                .IsRequired()
                .HasColumnName("Contraseña")
                .HasAnnotation("RequiredErrorMessage", "Contraseña is required");

            userEntity
                .Property(user => user.Distributor)
                .IsRequired()
                .HasColumnName("Distribuidora")
                .HasAnnotation("RequiredErrorMessage", "Distribuidora is required")
                .IsRequired();

            userEntity
                .Property(user => user.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasAnnotation("RequiredErrorMessage", "FechaAlta is required");

            userEntity
                .Property(user => user.SendErrorByEmail)
                .HasColumnName("EnviarErroresPorEmail")
                .HasAnnotation("RequiredErrorMessage", "SendErrorByEmail is required");

            userEntity
                .Property(user => user.Salt)
                .HasColumnName("Salt")
                .HasAnnotation("RequiredErrorMessage", "Salt is required");

            return modelBuilder;
        }
    }
}