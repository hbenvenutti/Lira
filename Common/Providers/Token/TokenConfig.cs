using Lira.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Lira.Common.Providers.Token;

public class TokenConfig
{
    public string PrivateKey { get; init; }
    public DateTime Expires => DateTime.UtcNow.AddHours(12);


    public TokenConfig(IConfiguration configuration)
    {
        PrivateKey = configuration["Token:PrivateKey"] 
            ?? throw new MissingEnvironmentVariableException(nameof(PrivateKey));
    }
}
