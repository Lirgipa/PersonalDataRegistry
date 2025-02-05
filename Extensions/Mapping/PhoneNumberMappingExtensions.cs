using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Dto.PhoneNumber;

namespace PersonalDataDirectory.Extensions.Mapping;

public static class PhoneNumberMappingExtensions
{
    public static PhoneNumberDto ToDto(this PhoneNumber phone)
    {
        return new PhoneNumberDto
        {
            Type = phone.Type,
            Number = phone.Number
        };
    }

    public static PhoneNumber ToEntity(this PhoneNumberDto dto)
    {
        return new PhoneNumber
        {
            Type = dto.Type,
            Number = dto.Number
        };
    }
}