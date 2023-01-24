using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.Repositories
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly ProfilesDbContext _profileDbContext;
        public ReceptionistRepository(ProfilesDbContext profilesDbContext)
        {
            _profileDbContext = profilesDbContext;
        }
        public async Task<IEnumerable<Receptionist?>> GetAllReceptionists(CancellationToken cancellationToken)
        {
            return await _profileDbContext.Receptionists
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .ToListAsync();
        }

        public async Task<Receptionist?> GetReceptionist(int id, CancellationToken cancellationToken)
        {
            return await _profileDbContext.Receptionists
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == id);
        }

        public async Task<Receptionist?> CreateReceptionist(Receptionist receptionist, CancellationToken cancellationToken)
        {
            _profileDbContext.Receptionists.Add(receptionist);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Receptionists
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == receptionist.Id);
        }

        public async Task<Receptionist?> UpdateReceptionist(Receptionist receptionist, CancellationToken cancellationToken)
        {
            _profileDbContext.Receptionists.Update(receptionist);
            await _profileDbContext.SaveChangesAsync(cancellationToken);

            return await _profileDbContext.Receptionists
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile == receptionist);
        }

        public async Task<Receptionist?> DeleteReceptionist(int id, CancellationToken cancellationToken)
        {
            var receptionist = await _profileDbContext.Receptionists
                .Include(profile => profile.Account)
                .Include(profile => profile.Office)
                .FirstOrDefaultAsync(profile => profile.Id == id);

            _profileDbContext.Receptionists.Remove(receptionist);
            await _profileDbContext.SaveChangesAsync(cancellationToken);
            return receptionist;
        }


    }
}
