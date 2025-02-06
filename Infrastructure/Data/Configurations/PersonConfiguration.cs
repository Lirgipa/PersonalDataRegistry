using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Gender)
            .HasConversion(new EnumToStringConverter<Gender>())
            .IsRequired();
        
        builder.HasIndex(p => p.PersonalNumber)
            .IsUnique()
            .HasDatabaseName("IX_Person_PersonalNumber");

        builder.Property(p => p.PhotoPath)
            .HasMaxLength(255);

        builder.HasMany(p => p.PhoneNumbers)
            .WithOne(pn => pn.Person)
            .HasForeignKey(pn => pn.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.RelatedPersons)
            .WithOne(rp => rp.Person)
            .HasForeignKey(rp => rp.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}