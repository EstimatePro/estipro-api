using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EstiPro.Application.Abstractions;
using EstiPro.Domain.Abstractions;
using EstiPro.Infrastructure.Repositories;

namespace EstiPro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DB
        var connectionString = configuration.GetConnectionString("EstiProDb");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDbConnection>(factory =>
            factory.GetRequiredService<ApplicationDbContext>().Database.GetDbConnection());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // Add repositories
        services.AddTransient<IRoomRepository, RoomRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IVoteRepository, VoteRepository>();
        services.AddTransient<ISessionRepository, SessionRepository>();
        services.AddTransient<ITicketRepository, TicketRepository>();

        // Add health checks
        services
            .AddHealthChecks()
            .AddSqlServer(connectionString)
            .AddDbContextCheck<ApplicationDbContext>();

        return services;
    }
}