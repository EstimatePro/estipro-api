using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using EstiPro.Api.IntegrationTests.Extensions;
using EstiPro.Api.IntegrationTests.Factories;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Services.Interfaces;

namespace EstiPro.Api.IntegrationTests.Fixtures;

public sealed class WebApiFixture : StartupFixture<Program>
{
    private static Faker Faker { get; } = new();

    public static UserDto OwnerUser { get; private set; } = null!;

    public static UserDto RegularUser { get; private set; } = null!;

    public static List<UserDto> Users { get; private set; } = [];

    public Mock<IAuth0Service> Auth0ServiceMock { get; } = new();
    public Mock<IAuth0UserService> Auth0UserServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseContentRoot(".");
        builder.UseEnvironment("Tests");
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);
        ConfigureTestUsers(services);
    }

    private void ConfigureTestUsers(IServiceCollection services)
    {
        OwnerUser = new UserDto(Guid.Parse("a94d67e1-8f46-4fe7-9212-24703f971b4f"), Faker.Internet.Email(), Faker.Name.FullName());
        RegularUser = new UserDto(Guid.Parse("b3343e55-34d9-4864-983d-34d7f7a539d0"), Faker.Internet.Email(), Faker.Name.FullName());
        Users.Add(OwnerUser);
        Users.Add(RegularUser);

        services.AddSingleton(_ => Auth0ServiceMock.Object);
        services.AddSingleton(_ => Auth0UserServiceMock.Object);

        services
            .AddAuthentication(TestAuthHandler.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, _ => { });
    }
}
