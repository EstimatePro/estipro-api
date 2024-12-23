using FluentValidation;

namespace EstiPro.Application.DTOs.Rooms;

public record BaseRoomDto<T>(string Name) where T : BaseRoomDto<T>;

public class BaseRoomDtoValidator<T> : AbstractValidator<T> where T : BaseRoomDto<T>
{
    protected BaseRoomDtoValidator()
    {
        RuleFor(x => x.Name).Length(1, 100);
    }
}