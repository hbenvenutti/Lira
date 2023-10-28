using System.Text;
using Lira.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Application.Services.Token;

public class TokenConfig
{
    public const string Issuer = "Lira";
    public string PrivateKey { get; init; }
    public static DateTime Expires => DateTime.UtcNow.AddHours(12);
    public SigningCredentials Credentials => GenerateCredentials();
    public SymmetricSecurityKey Key => new(Encoding.UTF8.GetBytes(PrivateKey));


    public TokenConfig(IConfiguration configuration)
    {
        PrivateKey = configuration["Token:PrivateKey"] 
            ?? throw new MissingEnvironmentVariableException(nameof(PrivateKey));
    }

    private SigningCredentials GenerateCredentials()
    {
        return new SigningCredentials(
            key: Key,
            algorithm: SecurityAlgorithms.HmacSha256Signature
        );
    }
}
