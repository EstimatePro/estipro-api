using EstiPro.Api.Extensions;
using EstiPro.Application.DTOs.Rooms;
using EstiPro.Application.DTOs.Tickets;
using EstiPro.Application.DTOs.VotingItems;
using EstiPro.Application.Rooms.Commands.AddTicket;
using EstiPro.Application.Rooms.Commands.AddVote;
using EstiPro.Application.Rooms.Commands.CreateRoom;
using EstiPro.Application.Rooms.Commands.DeleteRoom;
using EstiPro.Application.Rooms.Commands.JoinRoom;
using EstiPro.Application.Rooms.Commands.LeaveRoom;
using EstiPro.Application.Rooms.Commands.UpdateRoom;
using EstiPro.Application.Rooms.Queries.GetRoom;
using FluentResults;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstiPro.Api.Controllers;

public class RoomsController : BaseApiController<RoomsController>
{
    #region ROOM

    [HttpPost]
    [SwaggerOperation(Summary = "Create room")]
    [ProducesResponseType<RoomDto>(200)]
    [ProducesResponseType<RoomDto>(400)]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto createRoomDto, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new CreateRoomCommand(userId, createRoomDto);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("{roomId:guid}")]
    [SwaggerOperation(Summary = "Get room")]
    [ProducesResponseType<RoomDto>(200)]
    [ProducesResponseType<RoomDto>(400)]
    public async Task<IActionResult> GetRoom(Guid roomId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var request = new GetRoomQuery(roomId, userId);
        var result = await Sender.Send(request, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPut("{roomId:guid}")]
    [SwaggerOperation(Summary = "Update room")]
    [ProducesResponseType<RoomDto>(200)]
    [ProducesResponseType<RoomDto>(400)]
    public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomDto updateRoomDto, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new UpdateRoomCommand(userId, updateRoomDto);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{roomId:guid}")]
    [SwaggerOperation(Summary = "Delete room")]
    [ProducesResponseType<IActionResult>(200)]
    public async Task<IActionResult> DeleteRoom(Guid roomId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new DeleteRoomCommand(roomId, userId);
        var result = await Sender.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    #endregion

    #region ATTENDANCE

    [HttpPost("{roomId:guid}/join")]
    [SwaggerOperation(Summary = "Join room")]
    [ProducesResponseType<Result>(200)]
    [ProducesResponseType<Result>(400)]
    public async Task<IActionResult> JoinRoom(Guid roomId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new JoinRoomCommand(roomId, userId);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPost("{roomId:guid}/leave")]
    [SwaggerOperation(Summary = "Leave room")]
    [ProducesResponseType<Result>(200)]
    [ProducesResponseType<Result>(400)]
    public async Task<IActionResult> LeaveRoom(Guid roomId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new LeaveRoomCommand(roomId, userId);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    #endregion

    #region TICKETS

    [HttpPost("{roomId:guid}/tickets")]
    [SwaggerOperation(Summary = "Add ticket")]
    [ProducesResponseType<TicketDto>(200)]
    [ProducesResponseType<TicketDto>(400)]
    public async Task<IActionResult> AddTicket(
        Guid roomId,
        [FromBody] TicketDtoCreate ticket,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new AddTicketCommand(roomId, userId, ticket);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    #endregion

    #region VOTING

    [HttpPost("{roomId:guid}/tickets/{ticketId:guid}/vote")]
    [SwaggerOperation(Summary = "Add vote")]
    [ProducesResponseType<TicketDto>(200)]
    [ProducesResponseType<TicketDto>(400)]
    public async Task<IActionResult> AddVote(
        Guid roomId,
        Guid ticketId,
        [FromBody] AddVoteDto vote,
        CancellationToken cancellationToken)
    {
        var userId = User.GetUserGuid();
        var command = new AddVoteCommand(roomId, ticketId, userId, vote);
        var result = await Sender.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    #endregion
}