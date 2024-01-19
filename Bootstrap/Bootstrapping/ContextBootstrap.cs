using System.Diagnostics.CodeAnalysis;
using Lira.Bootstrap.Config;
using Lira.Data.Contexts;
using Lira.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lira.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class ContextBootstrap
{
    # region ---- connection ---------------------------------------------------

    public static void ConfigureDbContext(
        this IServiceCollection services
    )
    {
        var connectionStringConfig = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<ConnectionStringConfig>>()
            .Value;

        var connectionString = connectionStringConfig.DefaultConnection;

        services.AddDbContext<IDbContext, LiraDbContext>(
            optionsAction: options => options.UseNpgsql(connectionString),
            ServiceLifetime.Transient
        );
    }

    # endregion

    # region ---- services -----------------------------------------------------

    public static void StartDatabase(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();

        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<LiraDbContext>();

        dbContext.MigrateDatabaseOnStartUp();
        dbContext.SeedDatabaseOnStartUpAsync();
    }

    # endregion

    # region ---- migration ----------------------------------------------------

    private static void MigrateDatabaseOnStartUp(
        this IDbContext dbContext
    )
    {
        dbContext.Database.Migrate();
    }

    # endregion

    # region ---- seed ---------------------------------------------------------

    private static async void SeedDatabaseOnStartUpAsync(
        this IDbContext dbContext
    )
    {
        await dbContext.SeedDatabaseAsync();
    }

    # endregion
}
