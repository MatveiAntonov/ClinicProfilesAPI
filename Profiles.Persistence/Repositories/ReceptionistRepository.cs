using Dapper;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Contexts;
using System.Data;
using System.Numerics;

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
            var query = "SELECT * " +
                "FROM [Receptionists] " +
                "JOIN Offices ON Receptionists.OfficeId = Offices.Id " +
                "JOIN Accounts ON Receptionists.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var receptionists = await connection.QueryAsync<Receptionist, Office, Account, Photo, Receptionist>(query,
                    (receptionist, office, account, photo) =>
                    {
                        receptionist.Office = office;
                        account.Photo = photo;
                        receptionist.Account = account;
                        return receptionist;
                    });
                return receptionists.ToList();
            }
        }

        public async Task<Receptionist?> GetReceptionist(int id, CancellationToken cancellationToken)
        {
            var query = "SELECT * " +
                "FROM [Receptionists] " +
                "JOIN Offices ON Receptionists.OfficeId = Offices.Id " +
                "JOIN Accounts ON Receptionists.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var receptionists = await connection.QueryAsync<Receptionist, Office, Account, Photo, Receptionist>(query,
                    (receptionist, office, account, photo) =>
                    {
                        receptionist.Office = office;
                        account.Photo = photo;
                        receptionist.Account = account;
                        return receptionist;
                    }, param: parameters);
                return receptionists.ToList().FirstOrDefault();
            }
        }

        public async Task<Receptionist?> CreateReceptionist(Receptionist receptionist, CancellationToken cancellationToken)
        {
            var query = "INSERT INTO [Receptionists] " +
                "(FirstName, LastName, MiddleName, " +
                "OfficeId, AccountId) " +
                "VALUES (@FirstName, @LastName, @MiddleName, " +
                "@OfficeId, @AccountId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", receptionist.FirstName, DbType.String);
            parameters.Add("LastName", receptionist.LastName, DbType.String);
            parameters.Add("MiddleName", receptionist.MiddleName, DbType.String);
            parameters.Add("OfficeId", receptionist.OfficeId, DbType.Int32);
            parameters.Add("AccountId", receptionist.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : receptionist;
            }
        }

        public async Task<Receptionist?> UpdateReceptionist(Receptionist receptionist, CancellationToken cancellationToken)
        {
            var query = "UPDATE [Receptionists] " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, " +
                "OfficeId = @OfficeId, AccountId = @AccountId " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", receptionist.Id, DbType.Int32);
            parameters.Add("FirstName", receptionist.FirstName, DbType.String);
            parameters.Add("LastName", receptionist.LastName, DbType.String);
            parameters.Add("MiddleName", receptionist.MiddleName, DbType.String);
            parameters.Add("OfficeId", receptionist.OfficeId, DbType.Int32);
            parameters.Add("AccountId", receptionist.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : receptionist;
            }
        }

        public async Task<int> DeleteReceptionist(int id, CancellationToken cancellationToken)
        {
            string query = "DELETE FROM [Receptionists] WHERE Id = @Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, new { Id = id });
                return r;
            }
        }


    }
}
