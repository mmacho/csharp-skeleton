using Aseme.Shared.Domain;
using Hsc.RolesFunctionalities.Domain;
using Hsc.UsersRoles.Domain;

namespace Hsc.Roles.Domain
{
    public class Role : LegacyEntity
    {
        public const string TableName = "Rol";

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserRoles> Users { get; set; }
        public virtual ICollection<RoleFunctionalities> Functionalities { get; set; }
    }
}