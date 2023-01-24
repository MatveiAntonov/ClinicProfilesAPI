using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Persistence.EntityTypeConfigurations;

namespace Profiles.Persistence.Contexts
{
    public class ProfilesDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new PatientProfileConfiguration());
            builder.ApplyConfiguration(new ReceptionistProfileConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
