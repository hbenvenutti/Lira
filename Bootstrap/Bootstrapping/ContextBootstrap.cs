using System.Diagnostics.CodeAnalysis;
using Lira.Data.Contexts;
using Lira.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class ContextBootstrap
{
    # region ---- connection ---------------------------------------------------

    public static void ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString(name: "DefaultConnection");

        services.AddDbContext<IDbContext, LiraDbContext>(
            optionsAction: options => options.UseNpgsql(connectionString),
            ServiceLifetime.Transient
        );
    }

    # endregion

    # region ---- services -----------------------------------------------------

    public static void StartDatabase(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateAsyncScope();

        using var dbContext = scope
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
