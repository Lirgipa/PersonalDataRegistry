using PersonalDataDirectory.Domain.Enums;
using PersonalDataDirectory.Dto.PhoneNumber;

namespace PersonalDataDirectory.Dtos.Person;

public class UpdatePersonDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string PersonalNumber { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public int CityId { get; set; }

    public List<PhoneNumberDto>? PhoneNumbers { get; set; }
}