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
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;
        public ReceptionistService(IReceptionistRepository repository)
        {
            _receptionistRepository = repository;
        }

        public async Task<IEnumerable<Receptionist?>> GetAllReceptionists()
        {
            var receptionists = await _receptionistRepository.GetAllReceptionists(default(CancellationToken));

            return receptionists;
        }

        public async Task<Receptionist?> GetReceptionistById(int id)
        {
            var receptionist = await _receptionistRepository.GetReceptionist(id, default(CancellationToken));

            return receptionist;
        }

        public async Task<Receptionist?> CreateReceptionist(Receptionist receptionist)
        {
            var createdReceptionist = await _receptionistRepository.CreateReceptionist(receptionist, default(CancellationToken));

            return createdReceptionist;
        }

        public async Task<Receptionist?> UpdateReceptionist(Receptionist receptionist)
        {
            var updatedReceptionist = await _receptionistRepository.UpdateReceptionist(receptionist, default(CancellationToken));

            return updatedReceptionist;
        }
        public async Task<int?> DeleteReceptionist(int id)
        {
            var rows = await _receptionistRepository.DeleteReceptionist(id, default(CancellationToken));

            return rows == 0 ? null : rows;
        }
    }
}
