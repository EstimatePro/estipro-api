using FluentValidation;

namespace EstiPro.Application.DTOs.CommonValidators;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("GUID must not be empty.");
    }
}
