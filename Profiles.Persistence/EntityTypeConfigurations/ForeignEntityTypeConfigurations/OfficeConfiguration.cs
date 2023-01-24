using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.Persistence.EntityTypeConfigurations.ForeignEntityTypeConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(office => office.Id);
            builder.HasIndex(office => office.Id)
                .IsUnique();
            builder.Property(office => office.OfficeName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
