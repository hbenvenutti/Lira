using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lira.Domain.Authentication.Manager;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Application.Services.Token;

public class TokenService : ITokenService
{
    private const string Issuer = "Lira";

    private readonly TokenConfig _tokenConfig;

    public TokenService(TokenConfig tokenConfig)
    {
        _tokenConfig = tokenConfig;
    }

    public string Sign(ManagerAuthDomain user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_tokenConfig.PrivateKey);

        var credentials = new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256Signature
        );

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(user),
            SigningCredentials = credentials,
            Issuer = TokenConfig.Issuer,
            Expires = TokenConfig.Expires
        };

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    public bool Verify(string token)
    {
        throw new NotImplementedException();
    }

    private static ClaimsIdentity GenerateClaimsIdentity(ManagerAuthDomain user)
    {


        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(
            type: ClaimTypes.Name,
            value: user.Username
        ));

        claimsIdentity.AddClaim(new Claim(
            type: ClaimTypes.NameIdentifier,
            value: user.Id.ToString()
        ));

        return claimsIdentity;
    }
}
