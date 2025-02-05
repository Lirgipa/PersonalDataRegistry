using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Dto.PhoneNumber;

public class PhoneNumberDto
{
    public PhoneNumberType Type { get; set; }
    public string Number { get; set; } = string.Empty;
}