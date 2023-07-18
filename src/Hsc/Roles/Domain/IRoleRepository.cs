namespace Hsc.Roles.Domain
{
    public interface IRoleRepository
    {
        Role GetRoleWithFunctionalities(int roleId);
    }
}