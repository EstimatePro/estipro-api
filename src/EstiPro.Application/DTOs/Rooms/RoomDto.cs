using EstiPro.Application.DTOs.Sessions;
using EstiPro.Application.DTOs.Tickets;

namespace EstiPro.Application.DTOs.Rooms;

public record RoomDto(
    Guid Id,
    string Name,
    Guid CreatedByUserId,
    List<TicketDto>? Tickets,
    List<SessionDto>? Sessions) : BaseRoomDto<RoomDto>(Name);
