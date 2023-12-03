using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Application.Services.Token;

[ExcludeFromCodeCoverage]
public class TokenConfig
{
    public const string SectionName = "Token";

    # region ---- properties ---------------------------------------------------

    [Required]
    public required string PrivateKey { get; init; }

    [Required]
    public required string Issuer { get; init; }

    [Required]
    public required string Audience { get; init; }

    public static DateTime Expires => DateTime.UtcNow.AddHours(12);
    public SigningCredentials Credentials => GenerateCredentials();
    public SymmetricSecurityKey Key => new(Encoding.UTF8.GetBytes(PrivateKey));

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
