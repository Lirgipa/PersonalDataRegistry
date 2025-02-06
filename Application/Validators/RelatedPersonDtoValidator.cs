using FluentValidation;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Application.Validators;

public class RelatedPersonDtoValidator : AbstractValidator<RelatedPersonDto>
{
    public RelatedPersonDtoValidator()
    {
        RuleFor(r => r.RelationshipType)
            .IsInEnum();

        RuleFor(r => r.RelatedPersonId)
            .GreaterThan(0);
    }
}