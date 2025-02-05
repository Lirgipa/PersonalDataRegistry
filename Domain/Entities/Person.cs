using System.ComponentModel.DataAnnotations;
using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Domain.Entities;

public class Person
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    [RegularExpression("^[ა-ჰ]+$|^[A-Za-z]+$", ErrorMessage = "Only Georgian or Latin letters allowed, but not both together.")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    [RegularExpression("^[ა-ჰ]+$|^[A-Za-z]+$", ErrorMessage = "Only Georgian or Latin letters allowed, but not both together.")]
    public string LastName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public Gender Gender { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string PersonalNumber { get; set; }

    [Required] 
    [DataType(DataType.Date)] 
    public DateTime DateOfBirth { get; set; }

    [Required] public int CityId { get; set; }

    public List<PhoneNumber> PhoneNumbers { get; set; } = [];

    [MaxLength(255)]
    public string? PhotoPath { get; set; }

    public List<RelatedPerson> RelatedPersons { get; set; } = [];

    public bool IsAdult()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }
        return age >= 18;
    }
}