using System.Net.Http.Json;
using AutoBogus;
using FluentAssertions;
using Moq;
using Poke.Api.IntegrationTests.Fixtures;
using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Xunit.Abstractions;

namespace Poke.Api.IntegrationTests.Controllers;

[Collection(nameof(ControllerCollection))]
public class UsersControllerTests(
    ITestOutputHelper testOutputHelper,
    WebApiFixture webApiFixture)
    : ControllerTestsBase(testOutputHelper, webApiFixture)
{
    private const string BasicUrl = "api/users";

    private HttpClient HttpClient { get; set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        HttpClient = WebApiFixture.CreateJwtBearerClient(WebApiFixture.OwnerUser);
    }

    public override async Task DisposeAsync()
    {
        HttpClient.Dispose();
        await base.DisposeAsync();
    }

    [Fact]
    public async Task PostUser_ShouldCreateAUsers()
    {
        // Arrange
        var createdUserData = new AutoFaker<RegisterUserDto>()
            .RuleFor(f => f.NickName, f => f.Name.FullName())
            .RuleFor(f => f.Email, f => f.Internet.Email())
            .RuleFor(f => f.Password, f => f.Internet.Password())
            .Generate();

        WebApiFixture.Auth0ServiceMock
            .Setup(x => x.RegisterUser(It.IsAny<UserRegistrationDataDto>()))
            .ReturnsAsync(new AutoFaker<UserDto>().Generate());

        // Act
        var response = await HttpClient.PostAsJsonAsync(BasicUrl + "/signup", createdUserData);

        // Assert
        response.EnsureSuccessStatusCode();

        var createdUser =
            await response.Content.ReadFromJsonAsync<UserDto>();
        createdUser!.Should().NotBeNull();
        createdUser!.Id.Should().NotBe(Guid.Empty);
        createdUser.NickName.Should().BeEquivalentTo(createdUserData.NickName);
        createdUser.Email.Should().BeEquivalentTo(createdUserData.Email);
    }
}
