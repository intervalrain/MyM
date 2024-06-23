using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Transactions.FetchAllTransactions.Queries;

public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, ErrorOr<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransactionAsync(request.TransactionId, cancellationToken);

        return transaction == null ? Error.NotFound(description: "Transaction not found.") : TransactionDto.ToDto(transaction);
    }
}

