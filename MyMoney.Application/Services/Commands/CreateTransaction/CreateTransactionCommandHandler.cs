using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;
using MyMoney.Domain;

namespace MyMoney.Application.Services.CreateTransaction.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ErrorOr<TransactionDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public CreateTransactionCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (user == null) 
        {
            return Error.NotFound(description: "User not found");
        }

        if (!user.AccountIds.TryGetValue(request.AccountName, out Guid accountId))
        {
            return Error.NotFound(description: "Account not found");
        }

        var account = await _accountRepository.GetAccountAsync(accountId, cancellationToken);

        var transaction = new Transaction(request.UserId, accountId, request.Category, request.Amount, request.DateTime);

        var result = account!.AddTransaction(transaction);

        if (result.IsError)
        {
            return result.Errors; 
        }

        await _accountRepository.UpdateAccountAsync(account, cancellationToken);

        return TransactionDto.ToDto(transaction);
    }
}