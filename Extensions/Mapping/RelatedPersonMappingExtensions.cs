using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Extensions.Mapping;

public static class RelatedPersonMappingExtensions
{
    public static RelatedPersonDto ToDto(this RelatedPerson relatedPerson)
    {
        return new RelatedPersonDto
        {
            RelationshipType = relatedPerson.RelationshipType,
            RelatedPersonId = relatedPerson.RelatedPersonId
        };
    }

    public static RelatedPerson ToEntity(this RelatedPersonDto dto)
    {
        return new RelatedPerson
        {
            RelationshipType = dto.RelationshipType,
            RelatedPersonId = dto.RelatedPersonId
        };
    }
}