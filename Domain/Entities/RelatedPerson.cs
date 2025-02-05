using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Domain.Entities;

public class RelatedPerson
{
    public int Id { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int RelatedPersonId { get; set; }
    public Person RelatedToPerson { get; set; }
}