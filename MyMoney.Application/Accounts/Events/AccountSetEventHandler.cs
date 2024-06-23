using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain.Events;

namespace MyMoney.Application.Accounts.Events;

public class AccountSetEventHandler : INotificationHandler<AccountSetEvent>
{
    private readonly IAccountRepository _accountRepository;

    public AccountSetEventHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Handle(AccountSetEvent notification, CancellationToken cancellationToken)
    {
        await _accountRepository.AddAccountAsync(notification.Account, cancellationToken);
    }
}