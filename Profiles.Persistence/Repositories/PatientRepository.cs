using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ProfilesDbContext _profileDbContext;

        public PatientRepository(ProfilesDbContext profilesDbContext)
        {
            _profileDbContext = profilesDbContext;
        }
        public async Task<IEnumerable<Patient?>> GetAllPatients(CancellationToken cancellationToken)
        {
            return await _profileDbContext.Patients
                .Include(profile => profile.Account)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatient(int id, CancellationToken cancellationToken)
        {
            return await _profileDbContext.Patients
                .Include(profile => profile.Account)
                .FirstOrDefaultAsync(profile => profile.Id == id);
        }

        public async Task<Patient?> CreatePatient(Patient patient, CancellationToken cancellationToken)
        {
            _profileDbContext.Patients.Add(patient);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Patients
                .Include(profile => profile.Account)
                .FirstOrDefaultAsync(profile => profile.Id == patient.Id);
        }

        public async Task<Patient?> UpdatePatient(Patient patient, CancellationToken cancellationToken)
        {
            _profileDbContext.Patients.Update(patient);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Patients
                .Include(profile => profile.Account)
                .FirstOrDefaultAsync(profile => profile == patient);
        }

        public async Task<Patient?> DeletePatient(int id, CancellationToken cancellationToken)
        {
            var patient = await _profileDbContext.Patients
                .Include(profile => profile.Account)
                .FirstOrDefaultAsync(profile => profile.Id == id);

            _profileDbContext.Patients.Remove(patient);
            await _profileDbContext.SaveChangesAsync(cancellationToken);
            return patient;
        }
    }
}
