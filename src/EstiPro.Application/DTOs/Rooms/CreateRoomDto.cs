namespace EstiPro.Application.DTOs.Rooms;

public sealed record CreateRoomDto(string Name) : BaseRoomDto<CreateRoomDto>(Name);

public class CreateRoomDtoValidator : BaseRoomDtoValidator<CreateRoomDto>
{
    public CreateRoomDtoValidator()
    {
    }
}