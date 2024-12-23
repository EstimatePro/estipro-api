using EstiPro.Application.DTOs.Sessions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Mappers;

public static class SessionMapper
{
    public static SessionDto SessionToSessionDto(Session session)
    {
        return new SessionDto(session.Id, session.RoomId, session.UserId, session.UserRole);
    } 
}