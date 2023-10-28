using Lira.Data.Contexts;
using Lira.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lira.Bootstrap.Bootstrapping;

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
            optionsAction: options => options.UseNpgsql(connectionString)
        );
    }

    # endregion

    # region ---- migration ----------------------------------------------------

    public static void MigrateDatabaseOnStartUp(
        this IApplicationBuilder builder
    )
    {
        using var scope = builder.ApplicationServices.CreateAsyncScope();

        using var contentContext = scope
            .ServiceProvider
            .GetRequiredService<LiraDbContext>();

        contentContext.Database.Migrate();
    }

    # endregion

    # region ---- seed ---------------------------------------------------------

    public static async void SeedDatabaseOnStartUp(
        this IApplicationBuilder builder
    )
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();

        await using var contentContext = scope
            .ServiceProvider
            .GetRequiredService<LiraDbContext>();

        await contentContext.SeedDatabase();
    }

    # endregion
}
