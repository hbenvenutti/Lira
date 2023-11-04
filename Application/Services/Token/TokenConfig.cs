using System.Text;
using Lira.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Application.Services.Token;

public class TokenConfig
{
    # region ---- properties ---------------------------------------------------

    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string PrivateKey { get; init; }
    public static DateTime Expires => DateTime.UtcNow.AddHours(12);
    public SigningCredentials Credentials => GenerateCredentials();
    public SymmetricSecurityKey Key => new(Encoding.UTF8.GetBytes(PrivateKey));

    # endregion

    # region ---- constructor --------------------------------------------------

    public TokenConfig(IConfiguration configuration)
    {
        PrivateKey = configuration["Token:PrivateKey"]
            ?? throw new MissingEnvironmentVariableException(nameof(PrivateKey));

        Issuer = configuration["Token:Issuer"]
            ?? throw new MissingEnvironmentVariableException(nameof(Issuer));

        Audience = configuration["Token:Audience"]
            ?? throw new MissingEnvironmentVariableException(nameof(Audience));
    }

    # endregion

    # region ---- credentials --------------------------------------------------

    private SigningCredentials GenerateCredentials()
    {
        return new SigningCredentials(
            key: Key,
            algorithm: SecurityAlgorithms.HmacSha256Signature
        );
    }

    # endregion
}
