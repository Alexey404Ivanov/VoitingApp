using Askly.Application.Interfaces.Repositories;
using Askly.Domain;

namespace Askly.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public Task Add(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }
}