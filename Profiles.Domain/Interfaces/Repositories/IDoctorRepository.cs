using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor?>> GetAllDoctors(CancellationToken cancellationToken);
        Task<Doctor?> GetDoctor(int id, CancellationToken cancellationToken);
        Task<Doctor?> CreateDoctor(Doctor doctor, CancellationToken cancellationToken);
        Task<Doctor?> UpdateDoctor(Doctor doctor, CancellationToken cancellationToken);
        Task<int> DeleteDoctor(int id, CancellationToken cancellationToken);
    }
}
