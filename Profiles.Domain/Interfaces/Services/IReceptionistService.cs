using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Services
{
    public interface IReceptionistService
    {
        Task<IEnumerable<Receptionist?>> GetAllReceptionists();
        Task<Receptionist?> GetReceptionistById(int id);
        Task<Receptionist?> CreateReceptionist(Receptionist receptionist);
        Task<Receptionist?> UpdateReceptionist(Receptionist receptionist);
        Task<int?> DeleteReceptionist(int id);
    }
}
