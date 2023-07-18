using Aseme.Shared.Domain.Support;
using Hsc.UsersRoles.Domain;

namespace Hsc.Logins.Domain
{
    public class Login : LegacyEntity
    {
        public const string TableName = "Logins";

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int AuthorizedThirdPartyFilterMode { get; set; }

        public virtual IEnumerable<UserRoles> Roles { get; set; }
    }
}