using Lira.Bootstrap.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class OptionBootstrap
{
    public static void ConfigureOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOptions<ConnectionStringConfig>()
            .Bind(configuration
                .GetRequiredSection(key: ConnectionStringConfig.SectionName)
            )
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
