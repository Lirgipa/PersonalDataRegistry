using PersonalDataDirectory.Domain.Enums;
using PersonalDataDirectory.Dto.PhoneNumber;
using PersonalDataDirectory.Dtos.RelatedPerson;

namespace PersonalDataDirectory.Dtos.Person;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberDto> PhoneNumbers { get; set; } = [];
    public string? PhotoPath { get; set; }
    public List<RelatedPersonDto> RelatedPersons { get; set; } = [];
}