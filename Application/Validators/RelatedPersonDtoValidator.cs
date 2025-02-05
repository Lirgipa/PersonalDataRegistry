using FluentValidation;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Application.Validators;

public class RelatedPersonDtoValidator : AbstractValidator<RelatedPersonDto>
{
    public RelatedPersonDtoValidator()
    {
        RuleFor(r => r.RelationshipType)
            .IsInEnum().WithMessage("Invalid relationship type.");

        RuleFor(r => r.RelatedPersonId)
            .GreaterThan(0).WithMessage("Related person ID must be a valid positive integer.");
    }
}