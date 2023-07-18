using Aseme.Shared.Domain.Exceptions;
using Hsc.Logins.Domain;

namespace Hsc.Logins.Aplication.Get
{
    public class GetLoginService : IGetLoginService
    {
        private readonly ILoginRepository _repository;

        public GetLoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<Login> GetAsync(int id)
        {
            return await FindUserIfExists(id);
        }

        private async Task<Login> FindUserIfExists(int id)
        {
            Login entity = await _repository.FindAsync(new LoginWithLoginDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, Login.TableName, id); }
            return entity;
        }
    }
}