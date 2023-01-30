using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient?>> GetAllPatients(CancellationToken cancellationToken);
        Task<Patient?> GetPatient(int id, CancellationToken cancellationToken);
        Task<Patient?> CreatePatient(Patient patient, CancellationToken cancellationToken);
        Task<Patient?> UpdatePatient(Patient patient, CancellationToken cancellationToken);
        Task<int> DeletePatient(int id, CancellationToken cancellationToken);
    }
}
