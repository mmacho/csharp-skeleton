using Aseme.Shared.Domain;
using Hsc.RolesFunctionalities.Domain;

namespace Hsc.Functionalities.Domain
{
    public class Functionality : LegacyEntity
    {
        public const string TableName = "Funcionalidad";

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RoleFunctionalities> Roles { get; set; }
    }
}