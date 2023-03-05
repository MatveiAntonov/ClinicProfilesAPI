using AutoMapper;
using Events;
using MassTransit;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;

namespace Profiles.Application.Consumer.Events.Accounts
{
    public class AccountUpdatedConsumer : IConsumer<AccountUpdated>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountUpdatedConsumer(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AccountUpdated> context)
        {
            var account = _mapper.Map<Account>(context.Message);
            if (account is not null)
            {
                _accountRepository.UpdateAccountAsync(account);
            }
        }
    }
}
