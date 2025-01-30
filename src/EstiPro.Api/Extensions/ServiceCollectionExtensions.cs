using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using EstiPro.Api.Authorization;
using EstiPro.Api.Options;
using FluentValidation.AspNetCore;
using ZiggyCreatures.Caching.Fusion;

namespace EstiPro.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration
            .GetSection(CacheOptions.SectionName)
            .Get<CacheOptions>();

        ArgumentNullException.ThrowIfNull(settings);

        var cacheOptions = new FusionCacheOptions
        {
            DefaultEntryOptions =
            {
                Duration = settings.Duration
            }
        };
        var cache = new FusionCache(cacheOptions);
        services.AddFusionCache(cache);

        return services;
    }

    public static IServiceCollection AddAuth0AuthenticationAndAuthorization(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.Authority = configuration["Auth0:Domain"];
                    options.Audience = configuration["Auth0:EstiPro:Audience"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            // Extract claims
                            var claims = context.Principal?.Claims.ToArray();
                            return Task.CompletedTask;
                        }
                    };
                });

        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        return services;
    }

    public static IServiceCollection AddBasicAuthenticationAndAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

        return services;
    }
    
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services
            .AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
            .AddApplicationPart(assembly);

        services.AddFluentValidationAutoValidation();
        services.AddEndpointsApiExplorer();

        return services;
    }
}