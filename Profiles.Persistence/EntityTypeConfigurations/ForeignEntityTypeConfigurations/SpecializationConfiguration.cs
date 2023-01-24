using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profiles.Domain.Entities.ForeignEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.EntityTypeConfigurations.ForeignEntityTypeConfigurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(specialization => specialization.Id);
            builder.HasIndex(specialization => specialization.Id)
                .IsUnique();
            builder.Property(specialization => specialization.SpecializationName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
