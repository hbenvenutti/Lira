using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Lira.Domain.Authentication.Manager;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Application.Services.Token;

public class TokenService : ITokenService
{
    # region ---- properties ---------------------------------------------------

    private readonly TokenConfig _tokenConfig;

    # endregion

    # region ---- constructor --------------------------------------------------

    public TokenService(TokenConfig tokenConfig)
    {
        _tokenConfig = tokenConfig;
    }

    # endregion

    # region ---- sign ---------------------------------------------------------

    public string Sign(ManagerAuthDomain user)
    {
        var handler = new JwtSecurityTokenHandler();

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(user),
            SigningCredentials = _tokenConfig.Credentials,
            Issuer = _tokenConfig.Issuer,
            Audience = _tokenConfig.Audience,
            Expires = TokenConfig.Expires
        };

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    # endregion

    # region ---- verify -------------------------------------------------------

    public bool Verify(string token)
    {
        throw new NotImplementedException();
    }

    # endregion

    # region ---- claims -------------------------------------------------------

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

    # endregion
}
