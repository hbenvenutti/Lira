using Lira.Common.Providers.HashProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class ProviderBootstrap
{
    public static void ConfigureProviders(
        this IServiceCollection services
    )
    {
        services.AddScoped<IHashProvider, HashProvider>();
    }
}
