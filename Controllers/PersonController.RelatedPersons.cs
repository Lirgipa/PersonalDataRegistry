using Microsoft.AspNetCore.Mvc;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Controllers;

public partial class PersonController
{
    [HttpPost("{personId}/related-persons")]
    public async Task<IActionResult> AddRelatedPerson(int personId, [FromBody] RelatedPersonDto dto)
    {
        await _relationshipService.AddRelationshipAsync(personId, dto.RelatedPersonId, dto.RelationshipType);
        return Ok(new { Message = "Related person added successfully." });
    }

    [HttpDelete("{personId}/related-persons/{relatedPersonId}")]
    public async Task<IActionResult> RemoveRelatedPerson(int personId, int relatedPersonId)
    {
        await _relationshipService.RemoveRelationshipAsync(personId, relatedPersonId);
        return Ok(new { Message = "Related person removed successfully." });
    }
}