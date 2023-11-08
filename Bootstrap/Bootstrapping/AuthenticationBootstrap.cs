using System.Diagnostics.CodeAnalysis;
using Lira.Application.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Lira.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class AuthenticationBootstrap
{
    public static void ConfigureAuthentication(
        this IServiceCollection services
    )
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults
                    .AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults
                    .AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                var tokenConfig = services
                    .BuildServiceProvider()
                    .GetRequiredService<TokenConfig>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenConfig.Key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}
