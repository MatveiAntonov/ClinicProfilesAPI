using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository repository)
        {
            _patientRepository = repository;
        }

        public async Task<IEnumerable<Patient?>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients(default(CancellationToken));

            return patients;
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetPatient(id, default(CancellationToken));

            return patient;
        }

        public async Task<Patient?> CreatePatient(Patient patient)
        {
            var createdPatient = await _patientRepository.CreatePatient(patient, default(CancellationToken));

            return createdPatient;
        }

        public async Task<Patient?> UpdatePatient(Patient patient)
        {
            var updatedPatient = await _patientRepository.UpdatePatient(patient, default(CancellationToken));

            return updatedPatient;
        }
        public async Task<int?> DeletePatient(int id)
        {
            var rows = await _patientRepository.DeletePatient(id, default(CancellationToken));

            return rows == 0 ? null : rows;
        }
    }
}
