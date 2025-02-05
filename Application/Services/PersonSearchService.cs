using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Application.Services;

public class PersonSearchService : IPersonSearchService
{
    public Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(PersonSearchDto searchDto)
    {
        throw new NotImplementedException();
    }
}