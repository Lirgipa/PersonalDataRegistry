using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Dto.Person;
using PersonalDataDirectory.Dtos.Person;

namespace PersonalDataDirectory.Extensions.Mapping;

public static class PersonMappingExtensions
{
    public static PersonDto ToDto(this Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Gender = person.Gender,
            PersonalNumber = person.PersonalNumber,
            DateOfBirth = person.DateOfBirth,
            CityId = person.CityId,
            PhoneNumbers = person.PhoneNumbers.Select(p => p.ToDto()).ToList(),
            PhotoPath = person.PhotoPath,
            RelatedPersons = person.RelatedPersons.Select(rp => rp.ToDto()).ToList()
        };
    }

    public static Person ToEntity(this CreatePersonDto personDto)
    {
        return new Person
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            Gender = personDto.Gender,
            PersonalNumber = personDto.PersonalNumber,
            DateOfBirth = personDto.DateOfBirth,
            CityId = personDto.CityId,
            PhoneNumbers = personDto.PhoneNumbers.Select(p => p.ToEntity()).ToList(),
            RelatedPersons = personDto.RelatedPersons.Select(p => p.ToEntity()).ToList()
        };
    }
}