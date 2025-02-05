using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Infrastructure.Data.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.HasKey(pn => pn.Id);
        
        builder.Property(pn => pn.Number)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(pn => pn.Type)
            .HasConversion(new EnumToStringConverter<PhoneNumberType>())
            .IsRequired();
        
        builder.HasOne(pn => pn.Person)
            .WithMany(p => p.PhoneNumbers)
            .HasForeignKey(pn => pn.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}