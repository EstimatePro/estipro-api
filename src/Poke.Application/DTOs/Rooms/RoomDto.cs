using Poke.Application.DTOs.Sessions;
using Poke.Application.DTOs.Tickets;

namespace Poke.Application.DTOs.Rooms;

public record RoomDto(
    Guid Id,
    string Name,
    Guid CreatedByUserId,
    List<TicketDto>? Tickets,
    List<SessionDto>? Sessions) : BaseRoomDto<RoomDto>(Name);
