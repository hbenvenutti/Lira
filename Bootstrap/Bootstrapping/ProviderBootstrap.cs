using Lira.Application.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class ProviderBootstrap
{
    public static void ConfigureProviders(
        this IServiceCollection services
    )
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<TokenConfig>();
    }
}
