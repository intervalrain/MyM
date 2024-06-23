using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Transactions.FetchAllTransactions.Queries;

public class FetchAllTransactionsQueryHandler : IRequestHandler<FetchAllTransactionsQuery, ErrorOr<List<TransactionDto>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public FetchAllTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<List<TransactionDto>>> Handle(FetchAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(request.UserId, cancellationToken);

        return transactions.ToList().ConvertAll(TransactionDto.ToDto);
    }
}

