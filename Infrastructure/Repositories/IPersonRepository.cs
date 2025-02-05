using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Dtos.Person;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Infrastructure.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<bool> ExistsByPersonalNumberAsync(string personalNumber);
    Task<Person?> GetByIdWithPhonesAsync(int id);
    Task<Person?> GetByIdWithDetailsAsync(int id);
    Task DeleteAsync(int id);
    Task DeleteRelationshipAsync(int personId, int relatedPersonId);
    Task<(List<Person> Persons, int TotalCount)> SearchPersonsAsync(PersonSearchDto searchDto);
    Task<List<RelationshipReportDto>> GetRelationshipReportAsync();

}