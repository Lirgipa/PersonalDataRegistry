using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Domain.Enums;
using PersonalDataDirectory.Infrastructure.Repositories;
using PersonalDataDirectory.Infrastructure.UnitOfWork;

namespace PersonalDataDirectory.Application.Services;

public class RelationshipService : IRelationshipService
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RelationshipService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddRelationshipAsync(int personId, int relatedPersonId, RelationshipType type)
    {
        if (personId == relatedPersonId)
            throw new ArgumentException("A person cannot be related to themselves.");

        var person = await _personRepository.GetByIdWithDetailsAsync(personId);
        var relatedPerson = await _personRepository.GetByIdAsync(relatedPersonId);

        if (person == null || relatedPerson == null)
            throw new KeyNotFoundException("One or both persons not found.");

        if (person.RelatedPersons.Any(rp => rp.RelatedPersonId == relatedPersonId))
            throw new InvalidOperationException("Relationship already exists.");

        person.RelatedPersons.Add(new RelatedPerson
        {
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = type,
            RelatedToPerson = relatedPerson
        });

        _personRepository.Update(person);
        
        //todo come up with updateAsync implementation
        await _unitOfWork.TryCommitAsync(() => Task.CompletedTask);
    }

    public async Task RemoveRelationshipAsync(int personId, int relatedPersonId)
    {
        await _unitOfWork.TryCommitAsync(async () =>
        {
            await _personRepository.DeleteRelationshipAsync(personId, relatedPersonId);
        });
    }
}