using Lira.Application.CQRS.Accounts.Commands.Login;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.CQRS.People.Commands.RegisterPerson;
using Lira.Application.CQRS.Phone.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class MediatorBootstrap
{
    public static void ConfigureMediatorServices(
        this IServiceCollection services
    )
    {
        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreateAdminHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(SignInHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(RegisterPersonHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreatePhoneHandler).Assembly)
        );
    }
}
