using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Dtos.RelatedPerson;

public class RelationshipReportDto
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Dictionary<RelationshipType, int> RelationshipCounts { get; set; } = new();
}
