using Microsoft.EntityFrameworkCore;
using Poke.Infrastructure;

namespace Poke.Api.Extensions;

public static class HostExtensions
{
    public static async Task ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
