using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Common.Providers.Token;

public class TokenService : ITokenService
{
    private readonly TokenConfig _tokenConfig;

    public TokenService(TokenConfig tokenConfig)
    {
        _tokenConfig = tokenConfig;
    }

    public string Sign(Guid userId)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_tokenConfig.PrivateKey);

        var credentials = new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256Signature
        );

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(userId),
            SigningCredentials = credentials,
            Expires = _tokenConfig.Expires
        };

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    public bool Verify(string token)
    {
        throw new NotImplementedException();
    }

    private static ClaimsIdentity GenerateClaimsIdentity(Guid userId)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(
            type: ClaimTypes.Name,
            value: userId.ToString()
        ));

        return claimsIdentity;
    }
}
