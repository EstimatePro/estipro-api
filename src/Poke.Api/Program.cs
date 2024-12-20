using Microsoft.Extensions.Options;
using Poke.Api.Extensions;
using Poke.Api.Logs;
using Poke.Api.Swagger;
using Poke.Application;
using Poke.Infrastructure;
using Poke.Presentation;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder
    .AddVaultConfiguration()
    .AddCustomLogging(builder)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddPresentation()
    .AddAuthorization()
    .AddCache(builder.Configuration)
    .AddBasicAuthenticationAndAuthorization()
    .AddAuth0AuthenticationAndAuthorization(builder.Configuration)
    .AddSwaggerGen()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, CustomSwaggerOptions>();

// Build and configure application
var app = builder.Build();
await app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapCustomHealthChecks();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

namespace Poke.Api
{
    public abstract class Program;
}
