using System.ComponentModel.DataAnnotations;
using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    [Required]
    public PhoneNumberType Type { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string Number { get; set; }
    [Required]
    public int PersonId { get; set; }
    public Person Person { get; set; }
}