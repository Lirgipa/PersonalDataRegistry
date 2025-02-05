using FluentValidation;
using PersonalDataDirectory.Dto.PhoneNumber;

namespace PersonalDataDirectory.Application.Validators;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(p => p.Number)
            .NotEmpty().WithMessage("Phone number is required.")
            .MinimumLength(4).MaximumLength(50)
            .WithMessage("Phone number must be between 4 and 50 characters.");
        
        RuleFor(p => p.Type)
            .IsInEnum().WithMessage("Invalid phone number type.");
    }
}