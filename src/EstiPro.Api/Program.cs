using EstiPro.Api.Extensions;
using EstiPro.Api.Observability;
using EstiPro.Application;
using EstiPro.Infrastructure;
using EstiPro.Presentation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder
    .AddVaultConfiguration()
    .AddCustomObservability(builder)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddPresentation()
    .AddAuthorization()
    .AddCache(builder.Configuration)
    .AddBasicAuthenticationAndAuthorization()
    .AddAuth0AuthenticationAndAuthorization(builder.Configuration)
    .AddOpenApi();

// Build and configure application
var app = builder.Build();
await app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.HideDownloadButton = true;
        options.WithHttpBearerAuthentication(authOptions => { authOptions.Token = "your-api-key"; });
    });
}

app.UseHttpsRedirection();
app.MapCustomHealthChecks();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

namespace EstiPro.Api
{
    public abstract class Program;
}