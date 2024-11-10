using FluentValidation;

namespace Poke.Application.DTOs.Rooms;

public sealed record UpdateRoomDto(string Name, Guid Id) : BaseRoomDto<UpdateRoomDto>(Name);

public class UpdateRoomDtoValidator : BaseRoomDtoValidator<UpdateRoomDto>
{
    public UpdateRoomDtoValidator()
    {
        RuleFor(x => x.Id).Must(x => Guid.Empty != x).WithMessage("Provide 'id'.");
    }
}