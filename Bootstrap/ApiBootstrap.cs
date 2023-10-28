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
    public static void ConfigureApi(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureProviders();
        // services.ConfigureMediatorServices();
        // services.ConfigureRepositories();
        // services.ConfigureProviders();
        services.ConfigureSwagger();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }

    public static void ConfigureApi(
        this IApplicationBuilder app,
        IWebHostEnvironment environment
    )
    {
        app.SeedDatabaseOnStartUp();
        app.ConfigureSwagger(environment);

        // app.ConfigureGlobalMiddlewares();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MigrateDatabaseOnStartUp();
    }


    public static void ConfigureControllers(
        this IEndpointRouteBuilder app
    )
    {
        app.MapControllers();
    }

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
    }}
