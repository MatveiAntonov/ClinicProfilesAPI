using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Persistence.Contexts;

namespace Profiles.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _profileDbContext;
        public DoctorRepository(ProfilesDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }
        public async Task<IEnumerable<Doctor?>> GetAllDoctors(CancellationToken cancellationToken)
        {
            return await _profileDbContext.Doctors
                .Include(profile => profile.Specialization)
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .ToListAsync();
        }
        public async Task<Doctor?> GetDoctor(int id, CancellationToken cancellationToken)
        {
            return await _profileDbContext.Doctors
                .Include(profile => profile.Specialization)
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == id);
        }

        public async Task<Doctor?> CreateDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            _profileDbContext.Doctors.Add(doctor);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Doctors
                .Include(profile => profile.Specialization)
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == doctor.Id);
        }
        public async Task<Doctor?> UpdateDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            _profileDbContext.Doctors.Update(doctor);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Doctors
                .Include(profile => profile.Specialization)
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile == doctor);
        }

        public async Task<Doctor?> DeleteDoctor(int id, CancellationToken cancellationToken)
        {
            var doctor = await _profileDbContext.Doctors
                .Include(profile => profile.Specialization)
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == id);
            
            _profileDbContext.Doctors.Remove(doctor);
            await _profileDbContext.SaveChangesAsync(cancellationToken);
            return doctor;
        }
    }
}
