using EstiPro.Domain.Enums;

namespace EstiPro.Application.DTOs.Sessions;

public record SessionDto(Guid Id, Guid RoomId, Guid UserId, Role Role);
