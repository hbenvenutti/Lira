using System.Diagnostics.CodeAnalysis;
using Lira.Application.CQRS.Accounts.Commands.Login;
using Lira.Application.CQRS.Address.Commands.CreateAddress;
using Lira.Application.CQRS.Emails.Commands.CreateEmail;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.CQRS.Managers.Commands.CreateManager;
using Lira.Application.CQRS.Medium.Commands.CreateMedium;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.CQRS.People.Commands.RegisterPerson;
using Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;
using Lira.Application.CQRS.Phone.Commands.CreatePhone;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
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

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreatePersonHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreateAddressHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreateManagerHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreateEmailHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreateMediumHandler).Assembly)
        );

        services.AddMediatR(mediatorServiceConfig => mediatorServiceConfig
            .RegisterServicesFromAssemblies(typeof(CreatePersonOrixaHandler).Assembly)
        );
    }
}
