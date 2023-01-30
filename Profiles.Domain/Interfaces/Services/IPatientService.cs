using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient?>> GetAllPatients();
        Task<Patient?> GetPatientById(int id);
        Task<Patient?> CreatePatient(Patient patient);
        Task<Patient?> UpdatePatient(Patient patient);
        Task<int?> DeletePatient(int id);
    }
}
