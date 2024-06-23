using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain.Events;

namespace MyMoney.Application.Transactions.Events;

public class TransactionSetEventHandler : INotificationHandler<TransactionSetEvent>
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionSetEventHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(TransactionSetEvent notification, CancellationToken cancellationToken)
    {
        await _transactionRepository.AddTransactionAsync(notification.Transaction, cancellationToken);
    }
}
