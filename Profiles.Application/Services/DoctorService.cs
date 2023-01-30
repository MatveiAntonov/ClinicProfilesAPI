using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Domain.Interfaces.Services;


namespace Profiles.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository repository)
        {
            _doctorRepository = repository;
        }

        public async Task<IEnumerable<Doctor?>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors(default(CancellationToken));

            return doctors;
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            var doctor = await _doctorRepository.GetDoctor(id, default(CancellationToken));

            return doctor;
        }

        public async Task<Doctor?> CreateDoctor(Doctor doctor)
        {
            var createdDoctor = await _doctorRepository.CreateDoctor(doctor, default(CancellationToken));

            return createdDoctor;
        }

        public async Task<Doctor?> UpdateDoctor(Doctor doctor)
        {
            var updatedDoctor = await _doctorRepository.UpdateDoctor(doctor, default(CancellationToken));

            return updatedDoctor;
        }
        public async Task<int?> DeleteDoctor(int id)
        {
            var rows = await _doctorRepository.DeleteDoctor(id, default(CancellationToken));

            return rows == 0 ? null : rows;
        }
    }
}
