using Microsoft.OpenApi.Models;

namespace Autoclouding.Auth.Api.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddCorsWithCustomizations(this IServiceCollection services, string corsPolicy)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy,
                builder => builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins("http://localhost:4200", "https://*.autoclouding.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build()
                    );
        });

        return services;
    }

    /// <summary>
    /// Adds Swagger with Customizations
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddSwaggerGenWithCustomizations(this IServiceCollection services, Config config)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{config.AzureAd.Instance}{config.AzureAd.TenantId}/oauth2/v2.0/authorize"),
                        Scopes = new Dictionary<string, string>
                        {
                            {$"{config.AzureAd.Audience}/{config.AzureAd.Scopes}", "Access to Cloud Portal API" }
                        }
                    }
                }
            });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }
}
