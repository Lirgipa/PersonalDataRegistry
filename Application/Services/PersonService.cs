using Microsoft.EntityFrameworkCore;
using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Domain.Enums;
using PersonalDataDirectory.Dto.Person;
using PersonalDataDirectory.Dtos.Person;
using PersonalDataDirectory.Extensions.Mapping;
using PersonalDataDirectory.Infrastructure.Repositories;
using PersonalDataDirectory.Infrastructure.UnitOfWork;

namespace PersonalDataDirectory.Application.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository, ILogger<PersonService> logger)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<int> CreateAsync(CreatePersonDto dto)
    {
        var personalNumberExistsTask = _personRepository.ExistsByPersonalNumberAsync(dto.PersonalNumber);

        var person = dto.ToEntity();

        if (!person.IsAdult())
            throw new ArgumentException("Person must be at least 18 years old.");

        var uniqueRelatedPersons = new HashSet<(int relatedId, RelationshipType type)>();

        foreach (var relatedPerson in person.RelatedPersons.ToList())
        {
            var existingPerson = await _personRepository.GetByIdAsync(relatedPerson.RelatedPersonId);
            if (existingPerson is null)
                throw new ArgumentException($"Related person with ID {relatedPerson.RelatedPersonId} does not exist.");

            var relationshipKey = (relatedPerson.RelatedPersonId, relatedPerson.RelationshipType);
            if (!uniqueRelatedPersons.Add(relationshipKey))
            {
                person.RelatedPersons.Remove(relatedPerson);
            }
        }

        await _unitOfWork.TryCommitAsync(async () =>
        {
            if (await personalNumberExistsTask)
                throw new ArgumentException($"Person with personal number {dto.PersonalNumber} already exists");

            await _personRepository.AddAsync(person);
        });

        return person.Id;
    }

    public async Task UpdateAsync(int id, UpdatePersonDto dto)
    {
        var existingPerson = await _personRepository.GetByIdWithPhonesAsync(id);

        if (existingPerson is null)
            throw new KeyNotFoundException($"Cannot update: Person with ID {id} does not exist.");

        if (!existingPerson.IsAdult())
            throw new InvalidOperationException("Person must be at least 18 years old.");

        if (await _personRepository.ExistsByPersonalNumberAsync(dto.PersonalNumber) &&
            existingPerson.PersonalNumber != dto.PersonalNumber)
        {
            throw new InvalidOperationException(
                $"A person with this personal number {dto.PersonalNumber} already exists.");
        }

        existingPerson.FirstName = dto.FirstName;
        existingPerson.LastName = dto.LastName;
        existingPerson.Gender = dto.Gender;
        existingPerson.PersonalNumber = dto.PersonalNumber;
        existingPerson.DateOfBirth = dto.DateOfBirth;
        existingPerson.CityId = dto.CityId;

        existingPerson.PhoneNumbers.Clear();
        if (dto.PhoneNumbers is not null)
        {
            existingPerson.PhoneNumbers.AddRange(dto.PhoneNumbers.Select(p => new PhoneNumber
            {
                Type = p.Type,
                Number = p.Number
            }));
        }

        await _unitOfWork.TryCommitAsync(() => Task.CompletedTask);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.TryCommitAsync(async () => await _personRepository.DeleteAsync(id));
    }

    public async Task<PersonDto?> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdWithDetailsAsync(id);

        return person?.ToDto();
    }

    public async Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(PersonSearchDto searchDto)
    {
        var (persons, totalCount) = await _personRepository.SearchPersonsAsync(searchDto);
        var personDtos = persons.Select(p => p.ToDto()).ToList();

        return (personDtos, totalCount);
    }
}