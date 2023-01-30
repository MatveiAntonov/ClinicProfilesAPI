using Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Interfaces.Repositories
{
    public interface IReceptionistRepository
    {
        Task<IEnumerable<Receptionist?>> GetAllReceptionists(CancellationToken cancellationToken);
        Task<Receptionist?> GetReceptionist(int id, CancellationToken cancellationToken);
        Task<Receptionist?> CreateReceptionist(Receptionist receptionist, CancellationToken cancellationToken);
        Task<Receptionist?> UpdateReceptionist(Receptionist receptionist, CancellationToken cancellationToken);
        Task<int> DeleteReceptionist(int id, CancellationToken cancellationToken);
    }
}
