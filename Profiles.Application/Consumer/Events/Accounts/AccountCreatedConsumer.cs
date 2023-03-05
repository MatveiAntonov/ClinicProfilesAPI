using AutoMapper;
using Events;
using MassTransit;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;

namespace Profiles.Application.Consumer.Events.Accounts
{
    public class AccountCreatedConsumer : IConsumer<AccountCreated>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountCreatedConsumer(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AccountCreated> context)
        {
            var account = _mapper.Map<Account>(context.Message);
            if (account is not null)
            {
                await _accountRepository.CreateAccount(account);
            }
        }
    }
}
