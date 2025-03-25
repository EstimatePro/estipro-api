using Microsoft.Extensions.Options;
using EstiPro.Api.Extensions;
using EstiPro.Api.Observability;
using EstiPro.Api.Swagger;
using EstiPro.Application;
using EstiPro.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;

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
    .AddSwaggerGen(opt => opt.SetupSwaggerDoc(builder.Environment))
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, CustomSwaggerOptions>();

// Build and configure application
var app = builder.Build();
await app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapCustomHealthChecks();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

namespace EstiPro.Api
{
    public abstract class Program;
}
