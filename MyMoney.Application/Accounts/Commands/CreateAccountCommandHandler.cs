using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;
using MyMoney.Domain;

namespace MyMoney.Application.Accounts.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ErrorOr<AccountDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandHandler(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: "User not found");
        }

        var accounts = await _accountRepository.GetAccountsByUserIdAsync(request.UserId, cancellationToken);

        if (accounts.Any(a => a.AccountName == request.AccountName))
        {
            return Error.Conflict(description: $"Account Name: {request.AccountName} has already existed.");
        }

        var account = new Account(request.UserId, request.AccountName, request.InitAmount);

        var result = user.AddAccount(account);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _userRepository.UpdateUserAsync(user, cancellationToken);

        return AccountDto.ToDto(account);
    }
}

