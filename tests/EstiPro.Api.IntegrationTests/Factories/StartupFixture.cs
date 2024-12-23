using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EstiPro.Api.IntegrationTests.Fixtures;
using EstiPro.Application.DTOs.Users;
using EstiPro.Infrastructure;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace EstiPro.Api.IntegrationTests.Factories;

public class StartupFixture<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    private WebApplicationFactoryClientOptions? _options;

    protected internal readonly PostgreSqlContainer PostgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .Build();

    public void Configure(
        ITestOutputHelper testOutputHelper,
        WebApplicationFactoryClientOptions? options = null)
    {
        _options = options ?? TestHttpClientExtensions.DefaultOptions;
        Server.PreserveExecutionContext = true;
    }

    public HttpClient CreateJwtBearerClient(UserDto user)
    {
        var token = GetJwtToken(user);
        return CreateJwtBearerClient(token);
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

        services.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseNpgsql(PostgreSqlContainer.GetConnectionString()));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(ConfigureTestServices);
    }

    private HttpClient CreateJwtBearerClient(string bearerToken)
    {
        return CreateClient(_options!).WithJwtBearer(bearerToken);
    }

    private string GetJwtToken(UserDto user)
    {
        return JwtTokenHelper.GetJwtToken(user);
    }

    public async Task InitializeAsync()
    {
        await PostgreSqlContainer.StartAsync().ConfigureAwait(false);
    }

    public new async Task DisposeAsync()
    {
        await PostgreSqlContainer.StopAsync().ConfigureAwait(false);
        await PostgreSqlContainer.DisposeAsync();
    }
}
