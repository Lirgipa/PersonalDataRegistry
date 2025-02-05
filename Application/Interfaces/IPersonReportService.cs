using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Application.Interfaces;

public interface IPersonReportService
{
    Task<List<RelationshipReportDto>> GetRelationshipReportAsync();

}