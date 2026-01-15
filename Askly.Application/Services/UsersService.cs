using Askly.Application.Interfaces.Auth;
using Askly.Application.Interfaces.Services;

namespace Askly.Application.Services;

public class UsersService : IUsersService
{
    private readonly IPasswordHasher _hasher;

    public UsersService(IPasswordHasher hasher)
    {
        _hasher = hasher;
    }
    
    public async Task Register(string userName, string email, string password)
    {
        
    }
}