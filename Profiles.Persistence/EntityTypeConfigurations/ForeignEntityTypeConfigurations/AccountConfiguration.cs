using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.EntityTypeConfigurations.ForeignEntityTypeConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(account => account.Id);
            builder.HasIndex(account => account.Id)
                .IsUnique();
            builder.Property(account => account.PhoneNumber)
                .HasMaxLength(13)
                .IsRequired(false);
        }
    }
}
