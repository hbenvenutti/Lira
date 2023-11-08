using Lira.Bootstrap.Bootstrapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lira.Bootstrap;

public static class ApiBootstrap
{
    # region ---- services -----------------------------------------------------

    public static void ConfigureApi(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureProviders();
        services.ConfigureMediatorServices();
        services.ConfigureRepositories();
        services.ConfigureSwagger();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.ConfigureAuthentication();
    }

    # endregion

    # region ---- app ----------------------------------------------------------

    public static void ConfigureApi(
        this IApplicationBuilder app,
        IWebHostEnvironment environment
    )
    {
        app.ConfigureSwagger(environment);

        app.ConfigureGlobalMiddleware();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.StartDatabase();
    }

    public static void ConfigureControllers(
        this IEndpointRouteBuilder app
    )
    {
        app.MapControllers();
    }

    # endregion

    # region ---- environment --------------------------------------------------

    public static void ConfigureApi(
        this IConfigurationBuilder configuration
    )
    {
        var environment = Environment
            .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? Environments.Development;

        configuration.AddJsonFile(
            path: "appsettings.json",
            optional: true
        );

        configuration.AddJsonFile(
            path: $"appsettings.{environment}.json",
            optional: true,
            reloadOnChange: true
        );
    }

    # endregion
}
