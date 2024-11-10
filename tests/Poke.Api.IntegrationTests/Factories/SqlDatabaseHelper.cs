using System.Linq.Expressions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Poke.Api.IntegrationTests.Fixtures;
using Poke.Infrastructure;

namespace Poke.Api.IntegrationTests.Factories;

internal static class SqlDatabaseHelper
{
    internal static async Task Truncate(WebApiFixture fixture)
    {
        await using var db = new NpgsqlConnection(fixture.PostgreSqlContainer.GetConnectionString());

        await db.ExecuteAsync(
            @"
                    DO $$
                    DECLARE
                        row record;
                    BEGIN
                        -- Iterates over each table in the public schema
                        FOR row IN SELECT tablename FROM pg_tables WHERE schemaname = 'public' AND tablename NOT LIKE 'pg_%'
                        LOOP
                            -- Constructs and executes a dynamic SQL command to truncate the table
                            EXECUTE 'TRUNCATE TABLE public.' || quote_ident(row.tablename) || ' CASCADE;';
                        END LOOP;
                    END $$;
        ");
    }

    internal static Task<T[]> FindRecords<T>(
        IServiceProvider serviceProvider,
        Expression<Func<T, bool>> filterPredicate) where T : class
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        return context.Set<T>().Where(filterPredicate).ToArrayAsync();
    }

    internal static async Task<T?> FindRecord<T>(
        IServiceProvider serviceProvider,
        Expression<Func<T, bool>> filterPredicate) where T : class
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<T>().FirstOrDefaultAsync(filterPredicate);
    }

    internal static async void AddRecords<T>(IServiceProvider serviceProvider, params T[] records) where T : class
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        foreach (var record in records)
        {
            var existingEntity = context.Set<T>()
                .Local.FirstOrDefault(
                    e =>
                        context.Entry(e).Metadata.FindPrimaryKey()!.Properties.Select(p => p.PropertyInfo?.GetValue(e))
                            .SequenceEqual(
                                context.Entry(record)
                                    .Metadata.FindPrimaryKey()
                                    ?.Properties
                                    .Select(p => p.PropertyInfo!.GetValue(record))!));

            if (existingEntity != null)
            {
                context.Entry(existingEntity).State = EntityState.Detached;
            }

            await context.AddAsync(record);
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle or log the exception as necessary
            throw new InvalidOperationException("Error saving changes to the database.", ex);
        }
    }
}
