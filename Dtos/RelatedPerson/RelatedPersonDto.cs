using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Dtos.RelatedPerson;

public class RelatedPersonDto
{
    public int RelatedPersonId { get; set; }
    public RelationshipType RelationshipType { get; set; }
}