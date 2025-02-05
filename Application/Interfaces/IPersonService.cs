using PersonalDataDirectory.Dto.Person;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Application.Interfaces;

public interface IPersonService
{
    Task<int> CreateAsync(CreatePersonDto dto);
    Task UpdateAsync(int id, UpdatePersonDto dto);
    Task DeleteAsync(int id);
    Task<PersonDto?> GetByIdAsync(int id);
}