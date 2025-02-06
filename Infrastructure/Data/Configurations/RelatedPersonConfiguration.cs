using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalDataDirectory.Domain.Entities;
using PersonalDataDirectory.Domain.Enums;

namespace PersonalDataDirectory.Infrastructure.Data.Configurations;

public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
{
    public void Configure(EntityTypeBuilder<RelatedPerson> builder)
    {
        builder.HasKey(rp => rp.Id);

        builder.HasIndex(index => new { index.PersonId, index.RelatedPersonId })
            .IsUnique();

        builder.Property(rp => rp.RelationshipType)
            .HasConversion(new EnumToStringConverter<RelationshipType>())
            .IsRequired();

        builder.HasOne(rp => rp.Person)
            .WithMany(p => p.RelatedPersons)
            .HasForeignKey(rp => rp.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rp => rp.RelatedToPerson)
            .WithMany()
            .HasForeignKey(rp => rp.RelatedPersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}