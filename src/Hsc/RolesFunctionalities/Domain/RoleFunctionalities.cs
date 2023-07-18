using Aseme.Shared.Domain.Support;
using Hsc.Functionalities.Domain;
using Hsc.Roles.Domain;

namespace Hsc.RolesFunctionalities.Domain
{
    public class RoleFunctionalities : LegacyEntity
    {
        public const string TableName = "RolFuncionalidad";

        public int RoleId { get; set; }

        public int FunctionalityId { get; set; }

        public string Value { get; set; }

        // TODO: Improve property name
        public int IsQueryable { get; set; }

        public virtual Role Role { get; set; }

        public virtual Functionality Functionality { get; set; }
    }
}