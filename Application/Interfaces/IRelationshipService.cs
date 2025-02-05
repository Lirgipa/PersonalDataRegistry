using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Application.Interfaces;

public interface IRelationshipService
{
    Task AddRelationshipAsync(int personId, int relatedPersonId, RelationshipType type);
    Task RemoveRelationshipAsync(int personId, int relatedPersonId);
}