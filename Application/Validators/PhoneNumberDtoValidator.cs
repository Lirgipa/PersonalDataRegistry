using FluentValidation;
using PersonalDataDirectory.Dto.PhoneNumber;

namespace PersonalDataDirectory.Application.Validators;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(p => p.Number)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);

        RuleFor(p => p.Type)
            .IsInEnum();
    }
}