using FluentValidation;

namespace EstiPro.Application.DTOs.Users;

public sealed record RegisterUserDto(string NickName, string Email, string Password);

public class AddUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public AddUserDtoValidator()
    {
        RuleFor(x => x.NickName).Length(1, 100);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).Length(5, 20);
    }
}
