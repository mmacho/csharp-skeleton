using Aseme.Shared.Domain.Support;

namespace Hsd.Users.Domain
{
    public class User : LegacyEntity
    {
        public const string TableName = "Usuarios";

        public string Name { get; set; }

        public string Password { get; set; }

        public string Distributor { get; set; }

        public string Email { get; set; }

        public bool SendErrorByEmail { get; set; }

        public string Salt { get; set; }
    }
}