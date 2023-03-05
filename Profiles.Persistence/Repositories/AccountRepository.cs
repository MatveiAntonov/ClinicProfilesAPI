using Dapper;
using Events;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Contexts;
using System.Data;

namespace Profiles.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ProfilesDbContext _profileDbContext;

        public AccountRepository(ProfilesDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            var query = "INSERT INTO [Accounts] " +
                "(Id, PhoneNumber, PhotoUrl) " +
                "VALUES (@Id, @PhoneNumber, @PhotoUrl)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", account.Id, DbType.String);
            parameters.Add("PhoneNumber", account.PhoneNumber, DbType.String);
            parameters.Add("PhotoUrl", account.PhotoUrl, DbType.String);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);

                return r == 0 ? false : true;
            }
        }

        public async Task<bool> UpdateAccountAsync(Account entity)
        {
            var query = "UPDATE [Accounts] " +
                "SET PhoneNumber = @PhoneNumber, PhotoUrl = @PhotoUrl " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.String);
            parameters.Add("PhoneNumber", entity.PhoneNumber, DbType.String);
            parameters.Add("PhotoUrl", entity.PhotoUrl, DbType.String);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? false : true;
            }
        }

        public async Task<bool> DeleteAccountAsync(Account account)
        {
            string query = "DELETE FROM [Accounts] WHERE Id = @Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, new { Id = account.Id });
                return r == 0 ? false : true;
            }
        }
    }
}
