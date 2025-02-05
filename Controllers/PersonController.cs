using Microsoft.AspNetCore.Mvc;
using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Dto.Person;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public partial class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;
    private readonly IPhotoService _photoService;
    private readonly IRelationshipService _relationshipService;
    private readonly IPersonSearchService _personSearchService;
    private readonly IPersonReportService _personReportService;

    public PersonController(IPersonService personService, ILogger<PersonController> logger, IPhotoService photoService, IRelationshipService relationshipService, IPersonSearchService personSearchService, IPersonReportService personReportService)
    {
        _personService = personService;
        _logger = logger;
        _photoService = photoService;
        _relationshipService = relationshipService;
        _personSearchService = personSearchService;
        _personReportService = personReportService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
    {
        var personId = await _personService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = personId }, dto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDto dto)
    {
        await _personService.UpdateAsync(id, dto);
        return Ok(new { Message = "Person updated successfully." });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await _personService.DeleteAsync(id);
        return Ok(new { Message = $"Person with id {id} deleted successfully." });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if (person is null)
            return NotFound(new { Error = $"Person with id {id} not found." });

        return Ok(person);
    }
}