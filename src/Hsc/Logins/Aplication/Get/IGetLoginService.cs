using Hsc.Logins.Domain;

namespace Hsc.Logins.Aplication.Get
{
    public interface IGetLoginService
    {
        Task<Login> GetAsync(int id);
    }
}