using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> CreateAccount(Account account);
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(Account account);
    }
}
