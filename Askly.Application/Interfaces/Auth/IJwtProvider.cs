using Askly.Domain;

namespace Askly.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);
}