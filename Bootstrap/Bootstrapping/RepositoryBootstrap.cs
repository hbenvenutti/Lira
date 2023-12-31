using Lira.Data.Repositories;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

public static class RepositoryBootstrap
{
    public static void ConfigureRepositories(
        this IServiceCollection services
    )
    {
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
