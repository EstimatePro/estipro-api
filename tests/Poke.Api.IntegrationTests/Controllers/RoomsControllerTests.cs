using System.Net.Http.Json;
using AutoBogus;
using FluentAssertions;
using Poke.Api.IntegrationTests.Factories;
using Poke.Api.IntegrationTests.Fixtures;
using Poke.Application.DTOs.Rooms;
using Poke.Application.DTOs.Tickets;
using Poke.Application.DTOs.VotingItems;
using Poke.Application.Mappers;
using Xunit.Abstractions;

namespace Poke.Api.IntegrationTests.Controllers;

[Collection(nameof(ControllerCollection))]
public class RoomsControllerTests(
    ITestOutputHelper testOutputHelper,
    WebApiFixture webApiFixture)
    : ControllerTestsBase(testOutputHelper, webApiFixture)
{
    private const string BasicUrl = "api/rooms";

    private HttpClient HttpClient { get; set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        HttpClient = WebApiFixture.CreateJwtBearerClient(WebApiFixture.OwnerUser);
        SqlDatabaseHelper.AddRecords(
            WebApiFixture.Services,
            WebApiFixture.Users.Select(UserMapper.UserDtoToUser).ToArray());
    }

    public override async Task DisposeAsync()
    {
        HttpClient.Dispose();
        await base.DisposeAsync();
    }

    [Fact]
    public async Task PostRoom_ShouldCreateRoom()
    {
        // Arrange
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();

        // Act
        var response = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);

        // Assert
        response.EnsureSuccessStatusCode();

        var createdRoomResult = await response.Content.ReadFromJsonAsync<RoomDto>();
        createdRoomResult.Should().NotBeNull();
        createdRoomResult!.Id.Should().NotBe(Guid.Empty);
        createdRoomResult.Sessions.Should()
            .ContainSingle()
            .Subject.UserId.Should()
            .Be(WebApiFixture.OwnerUser.Id);
    }

    [Fact]
    public async Task GetRoom_ShouldReturnRoom()
    {
        // Arrange
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom =
            await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();

        // Act
        var result = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom!.Id}");

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(createdRoom.Id);
        result.Name.Should().BeEquivalentTo(createRoomData.Name);
    }

    [Fact]
    public async Task DeleteRoom_ShouldDeleteRoom()
    {
        // Arrange
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom =
            await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();

        // Act
        var requestResult = await HttpClient.DeleteAsync($"{BasicUrl}/{createdRoom!.Id}");

        // Assert
        requestResult.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task PostJoin_ShouldJoinUserIntoRoom()
    {
        // Arrange
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom =
            await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();

        // Act
        HttpClient = WebApiFixture.CreateJwtBearerClient(WebApiFixture.RegularUser);
        var requestResult = await HttpClient.PostAsync($"{BasicUrl}/{createdRoom!.Id}/join", null);

        // Assert
        requestResult.IsSuccessStatusCode.Should().BeTrue();
        var roomResult = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom.Id}");
        roomResult!.Sessions?.Select(s => s.UserId).Should().Contain(WebApiFixture.Users.Select(u => u.Id));
    }

    [Fact]
    public async Task PostLeave_ShouldRemoveUserFromRoom()
    {
        // Arrange
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom =
            await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();
        HttpClient = WebApiFixture.CreateJwtBearerClient(WebApiFixture.RegularUser);
        await HttpClient.PostAsync($"{BasicUrl}/{createdRoom!.Id}/join", null);

        // Act
        var responseResult = await HttpClient.PostAsync($"{BasicUrl}/{createdRoom.Id}/leave", null);

        // Assert
        responseResult.IsSuccessStatusCode.Should().BeTrue();
        var roomResult = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom.Id}");
        roomResult!.Sessions?.Select(s => s.UserId).Should().NotContain(WebApiFixture.RegularUser.Id);
    }

    [Fact]
    public async Task PostTicket_ShouldAddTicket()
    {
        // Arrange
        var ticket = new AutoFaker<TicketDtoCreate>().Generate();
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom =
            await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();

        // Act
        var responseResult = await HttpClient.PostAsJsonAsync($"{BasicUrl}/{createdRoom!.Id}/tickets", ticket);

        // Assert
        responseResult.IsSuccessStatusCode.Should().BeTrue();
        var result = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom.Id}");
        result.Should().NotBeNull();
        result!.Tickets.Should()
            .ContainSingle(
                t => t.Title == ticket.Title && t.Description == ticket.Description);
    }

    [Fact]
    public async Task PostVote_ShouldAddVote()
    {
        // Arrange
        var newVote = new AutoFaker<AddVoteDto>().Generate();
        var createTicket = new AutoFaker<TicketDtoCreate>().Generate();
        var createRoomData = new AutoFaker<CreateRoomDto>().Generate();
        var createdRoomResponse = await HttpClient.PostAsJsonAsync(BasicUrl, createRoomData);
        var createdRoom = await createdRoomResponse.Content.ReadFromJsonAsync<RoomDto>();
        await HttpClient.PostAsJsonAsync($"{BasicUrl}/{createdRoom!.Id}/tickets", createTicket);
        var room = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom.Id}");
        var ticket = room!.Tickets!.First();

        // Act
        var responseResult =
            await HttpClient.PostAsJsonAsync($"{BasicUrl}/{createdRoom.Id}/tickets/{ticket.Id}/vote", newVote);

        // Assert
        await Task.Delay(1000);
        responseResult.IsSuccessStatusCode.Should().BeTrue();
        var result = await HttpClient.GetFromJsonAsync<RoomDto>($"{BasicUrl}/{createdRoom.Id}");
        result.Should().NotBeNull();
        result!.Tickets!.Single(t => t.Id == ticket.Id)
            .Votes
            .Should()
            .ContainSingle(vote => vote.UserId == WebApiFixture.OwnerUser.Id && vote.Mark == newVote.Mark);
    }
}