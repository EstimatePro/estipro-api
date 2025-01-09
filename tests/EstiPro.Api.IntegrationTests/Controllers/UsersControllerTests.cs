using System.Net.Http.Json;
using Auth0.ManagementApi.Models;
using AutoBogus;
using FluentAssertions;
using Moq;
using EstiPro.Api.IntegrationTests.Fixtures;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using Xunit.Abstractions;

namespace EstiPro.Api.IntegrationTests.Controllers;

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

        WebApiFixture.Auth0UserServiceMock
            .Setup(x => x.CreateUserAsync(It.IsAny<UserRegistrationDataDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AutoFaker<User>().Generate());

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
