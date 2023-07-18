using Aseme.Shared.Domain.Exceptions;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Roles.Domain
{
    public class RoleRepository : LegacyRepository<Role>, IRoleRepository
    {
        private new readonly HscDbContext _context;

        public RoleRepository(HscDbContext context) : base(context)
        {
            _context = context;
        }

        public Role GetRoleWithFunctionalities(int roleId)
        {
            return _context.Roles
                .Include(role => role.Functionalities)
                .FirstOrDefault(r => r.Id == roleId)
                ?? throw new NotFoundException(ErrorCode.NOT_FOUND, Role.TableName, roleId);
        }
    }
}