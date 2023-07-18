using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Hsc.Logins.Domain;
using Hsc.Roles.Domain;
using Hsc.RolesFunctionalities.Domain;
using Hsc.UsersRoles.Domain;

namespace Aseme.Apps.HubSupplier.Backend.Utils
{
    public class AuthorizationUtils
    {
        public static bool IsUserUnauthorized(Login user, RoleRepository roleRepository)
        {
            IEnumerable<UserRoles> roles = user.Roles;

            foreach (var userRole in roles)
            {
                Role role = roleRepository.GetRoleWithFunctionalities(userRole.RoleId);
                ICollection<RoleFunctionalities> functionalities = role.Functionalities;

                if (functionalities.Any(functionality => functionality.FunctionalityId == AuthorizationConstants.HUB_ACCESS_FUNCTIONALITY_IDENTIFIER))
                {
                    return false;
                }
            }

            return true;
        }
    }
}