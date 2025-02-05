using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Application.Interfaces;

public interface IPersonSearchService
{
    Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(PersonSearchDto searchDto);
}