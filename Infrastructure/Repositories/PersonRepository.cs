using Microsoft.EntityFrameworkCore;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Dtos.Person;
using PersonalDataDirectory.Dtos.RelatedPerson;
using PersonalDataDirectory.Infrastructure.Data;

namespace PersonalDataDirectory.Infrastructure.Repositories;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByPersonalNumberAsync(string personalNumber)
    {
        return await Context.PersonEntities.AnyAsync(p => p.PersonalNumber == personalNumber);
    }

    public async Task<Person?> GetByIdWithPhonesAsync(int id)
    {
        return await Context.PersonEntities.Include(p => p.PhoneNumbers).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person?> GetByIdWithDetailsAsync(int id)
    {
        var person = await Context.PersonEntities
            .Include(p => p.PhoneNumbers)
            .Include(p => p.RelatedPersons)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person is null)
            return null;

        var relatedAsPerson = await Context.RelatedPersons
            .Where(rp => rp.RelatedPersonId == id)
            .Include(rp => rp.Person)
            .ToListAsync();

        foreach (var rp in relatedAsPerson)
        {
            person.RelatedPersons.Add(new RelatedPerson
            {
                PersonId = id,
                RelatedPersonId = rp.PersonId,
                RelationshipType = rp.RelationshipType,
                RelatedToPerson = rp.Person
            });
        }

        return person;
    }

    public async Task DeleteAsync(int id)
    {
        var person = await Context.PersonEntities
            .Include(p => p.RelatedPersons)
            .Include(p => p.PhoneNumbers)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person is null)
            throw new KeyNotFoundException($"Person with ID {id} not found.");

        var allRelationships = person.RelatedPersons
            .Concat(await Context.RelatedPersons.Where(rp => rp.RelatedPersonId == id).ToListAsync())
            .ToList();

        Context.RelatedPersons.RemoveRange(allRelationships);
        Context.PhoneNumbers.RemoveRange(person.PhoneNumbers);
        Context.PersonEntities.Remove(person);
    }

    public async Task DeleteRelationshipAsync(int personId, int relatedPersonId)
    {
        var person = await Context.PersonEntities
            .Include(p => p.RelatedPersons)
            .FirstOrDefaultAsync(p => p.Id == personId);

        if (person == null)
            throw new KeyNotFoundException("Person not found.");

        var relationship = person.RelatedPersons.FirstOrDefault(rp => rp.RelatedPersonId == relatedPersonId);

        if (relationship == null)
            throw new KeyNotFoundException("Relationship not found.");

        person.RelatedPersons.Remove(relationship);
    }

    public async Task<(List<Person> Persons, int TotalCount)> SearchPersonsAsync(PersonSearchDto searchDto)
    {
        var query = Context.PersonEntities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchDto.FirstName))
            query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{searchDto.FirstName}%"));

        if (!string.IsNullOrWhiteSpace(searchDto.LastName))
            query = query.Where(p => EF.Functions.Like(p.LastName, $"%{searchDto.LastName}%"));

        if (!string.IsNullOrWhiteSpace(searchDto.PersonalNumber))
            query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{searchDto.PersonalNumber}%"));

        if (searchDto.Gender.HasValue)
            query = query.Where(p => p.Gender == searchDto.Gender);

        if (searchDto.CityId.HasValue)
            query = query.Where(p => p.CityId == searchDto.CityId);

        var totalCount = await query.CountAsync();

        var persons = await query
            .Include(p => p.PhoneNumbers)
            .Include(p => p.RelatedPersons)
            .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
            .Take(searchDto.PageSize)
            .ToListAsync();

        return (persons, totalCount);
    }

    public async Task<List<RelationshipReportDto>> GetRelationshipReportAsync()
    {
        var report = await Context.PersonEntities
            .Include(p => p.RelatedPersons)
            .Select(p => new RelationshipReportDto
            {
                PersonId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                RelationshipCounts = p.RelatedPersons
                    .GroupBy(rp => rp.RelationshipType)
                    .ToDictionary(g => g.Key, g => g.Count())
            })
            .ToListAsync();

        return report;
    }
}