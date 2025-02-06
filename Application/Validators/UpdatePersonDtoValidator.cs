using FluentValidation;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Application.Validators;

public class UpdatePersonDtoValidator : AbstractValidator<UpdatePersonDto>
{
    public UpdatePersonDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches("^[ა-ჰ]+$|^[A-Za-z]+$")
            .WithMessage("Only Georgian or Latin letters allowed, but not both together.")
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(p => p.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches("^[ა-ჰ]+$|^[A-Za-z]+$")
            .WithMessage("Only Georgian or Latin letters allowed, but not both together.")
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(p => p.PersonalNumber)
            .NotEmpty()
            .Length(11)
            .Matches("^[0-9]{11}$")
            .WithMessage("Personal Number must only contain digits.");

        RuleFor(p => p.DateOfBirth)
            .LessThan(DateTime.Today.AddYears(-18));

        RuleFor(p => p.PhoneNumbers)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Phone numbers list cannot be null.")
            .Must(phones => phones != null && phones.All(p => p.Number.Length is >= 4 and <= 50))
            .WithMessage("Phone numbers must be between 4 and 50 characters.");
    }
}