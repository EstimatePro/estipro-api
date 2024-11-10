using Poke.Application.DTOs.Sessions;
using Poke.Domain.Entities;

namespace Poke.Application.Mappers;

public static class SessionMapper
{
    public static SessionDto SessionToSessionDto(Session session)
    {
        return new SessionDto(session.Id, session.RoomId, session.UserId, session.UserRole);
    } 
}