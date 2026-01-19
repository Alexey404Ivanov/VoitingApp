using System.IdentityModel.Tokens.Jwt;
using Askly.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Askly.Application.Interfaces;
using Askly.Application.Interfaces.Auth;

namespace Askly.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    
    public string GenerateToken(UserEntity user)
    {
        var claims = new[]
        {
            new System.Security.Claims.Claim("userId", user.Id.ToString())
        };
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
            signingCredentials: signingCredentials);
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}