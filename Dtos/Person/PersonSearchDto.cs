using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Dtos.Person;

public class PersonSearchDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonalNumber { get; set; }
    public Gender? Gender { get; set; }
    public int? CityId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}