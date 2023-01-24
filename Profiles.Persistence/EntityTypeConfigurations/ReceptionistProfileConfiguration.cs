using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.EntityTypeConfigurations
{
    public class ReceptionistProfileConfiguration : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.HasKey(profile => profile.Id);
            builder.HasIndex(profile => profile.Id)
                .IsUnique();
            builder.Property(profile => profile.FirstName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(profile => profile.LastName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(profile => profile.MiddleName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
