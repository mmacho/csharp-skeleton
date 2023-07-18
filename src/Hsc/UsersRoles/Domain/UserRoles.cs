using Aseme.Shared.Domain.Support;
using Hsc.Logins.Domain;
using Hsc.Roles.Domain;

namespace Hsc.UsersRoles.Domain
{
    public class UserRoles : LegacyEntity
    {
        public const string TableName = "UsuarioRol";

        public int LoginUserId { get; set; }

        public int RoleId { get; set; }

        public virtual Login User { get; set; }

        public virtual Role Role { get; set; }
    }
}