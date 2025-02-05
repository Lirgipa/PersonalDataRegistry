using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Dtos.RelatedPerson;
using PersonalDataDirectory.Infrastructure.Repositories;

namespace PersonalDataDirectory.Application.Services;

public class PersonReportService : IPersonReportService
{
    private readonly IPersonRepository _personRepository;

    public PersonReportService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<RelationshipReportDto>> GetRelationshipReportAsync()
    {
        return await _personRepository.GetRelationshipReportAsync();
    }
}