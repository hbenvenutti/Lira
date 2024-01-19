using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class MediatorBootstrap
{
    public static void ConfigureMediatorServices(
        this IServiceCollection services
    )
    {
        services.AddMediatR(mediator => mediator
            .RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        );
    }
}
