using EstiPro.Application.DTOs.Rooms;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Mappers;

public static class RoomMapper
{
    public static RoomDto RoomToRoomDto(Room room)
    {
        return new RoomDto(
            room.Id,
            room.Name,
            room.CreatedByUserId,
            room.Tickets?.Select(TicketMapper.TicketToTicketDto).ToList(),
            room.Sessions?.Select(SessionMapper.SessionToSessionDto).ToList());
    }
}
