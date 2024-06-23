using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;
using MyMoney.Domain;

namespace MyMoney.Application.Transactions.CreateTransaction.Commands;

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

        var accounts = await _accountRepository.GetAccountsByUserIdAsync(request.UserId, cancellationToken);
        var account = accounts.FirstOrDefault(a => a.AccountName == request.AccountName);

        if (account == null)
        {
            return Error.NotFound(description: "Account not found");
        }

        var transaction = new Transaction(request.UserId, account.Id, request.Category, request.Amount, request.DateTime);

        var result = account!.AddTransaction(transaction);

        if (result.IsError)
        {
            return result.Errors; 
        }

        await _accountRepository.UpdateAccountAsync(account, cancellationToken);

        return TransactionDto.ToDto(transaction);
    }
}