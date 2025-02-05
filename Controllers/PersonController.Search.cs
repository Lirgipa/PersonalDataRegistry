using Microsoft.AspNetCore.Mvc;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Controllers;

public partial class PersonController
{
    [HttpGet("search")]
    public async Task<IActionResult> SearchPersons([FromQuery] PersonSearchDto searchDto)
    {
        var (persons, totalCount) = await _personSearchService.SearchPersonsAsync(searchDto);

        var response = new
        {
            TotalCount = totalCount,
            searchDto.PageNumber,
            searchDto.PageSize,
            Data = persons
        };

        return Ok(response);
    }
}