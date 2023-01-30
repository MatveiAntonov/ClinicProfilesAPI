using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor?>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(int id);
        Task<Doctor?> CreateDoctor(Doctor doctor);
        Task<Doctor?> UpdateDoctor(Doctor doctor);
        Task<int?> DeleteDoctor(int id);
    }
}
