using Lira.Application.Services.Token;
using Lira.Common.Providers.Hash;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class ProviderBootstrap
{
    public static void ConfigureProviders(
        this IServiceCollection services
    )
    {
        services.AddScoped<IHashService, HashService>();
        
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<TokenConfig>();
    }
}
