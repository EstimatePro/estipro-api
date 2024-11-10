using Poke.Domain.Enums;

namespace Poke.Application.DTOs.Sessions;

public record SessionDto(Guid Id, Guid RoomId, Guid UserId, Role Role);
